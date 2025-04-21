USE MASTER;

IF DB_ID('PerfilesDB') IS NULL
BEGIN
    CREATE DATABASE PerfilesDB;
END
GO

USE PerfilesDB;
GO

IF NOT EXISTS (
           SELECT TOP 1 1
             FROM sys.server_principals
            WHERE name = 'perfilesDB_usuario'
          )
BEGIN
    CREATE LOGIN perfilesDB_usuario
      WITH PASSWORD = 'abc123**';

    CREATE USER perfilesDB_usuario
       FOR LOGIN perfilesDB_usuario;

END
GO

EXEC sp_addrolemember 'db_owner', 'perfilesDB_usuario';
GO


IF OBJECT_ID('LogErrores') IS NULL
BEGIN
    CREATE TABLE dbo.LogErrores
    (
     idError       INT IDENTITY(1,1),
     fechaHora     DATETIME DEFAULT GETDATE(),
     mensajeError  NVARCHAR(MAX),
     lineaError    INT,
     procedimiento VARCHAR(4000),
     CONSTRAINT pk_logErrores PRIMARY KEY (idError)
    );
END;
GO

IF OBJECT_ID('Departamento') IS NULL
BEGIN
    CREATE TABLE dbo.Departamento
    (
     idDepartamento INTEGER IDENTITY(1,1),
     nombre         VARCHAR(400) NOT NULL,
     activo         BIT NOT NULL,
     CONSTRAINT pk_departamento PRIMARY KEY (idDepartamento),
     CONSTRAINT uq_departamento_nombre UNIQUE (nombre),
     CONSTRAINT chk_departamento_activo CHECK (activo IN (0, 1))
    );
END;
GO

IF OBJECT_ID('Empleado') IS NULL
BEGIN
    CREATE TABLE dbo.Empleado
    (
     idEmpleado      INTEGER IDENTITY(1,1),
     nombres         VARCHAR(80) NOT NULL,
     apellidos       VARCHAR(80) NOT NULL,
     fechaNacimiento DATE,
     sexo            BIT,
     activo          BIT NOT NULL,
     fechaIngreso    DATETIME NOT NULL,
     direccion       VARCHAR(400),
     nit             VARCHAR(20) NOT NULL,
     dpi             BIGINT NOT NULL,
     idDepartamento  INT,
     CONSTRAINT pk_empleado PRIMARY KEY (idEmpleado),
     CONSTRAINT fk_empleado_departamento FOREIGN KEY (idDepartamento) REFERENCES Departamento(idDepartamento),
     CONSTRAINT chk_empleado_sexo CHECK (sexo IN (0, 1)),
     CONSTRAINT chk_empleado_activo CHECK (activo IN (0, 1)),
     CONSTRAINT uq_empleado_nit UNIQUE (nit),
     CONSTRAINT uq_empleado_dpi UNIQUE (dpi)

    );
END;
GO


IF OBJECT_ID('trg_UpdateEmpleadoActivo') IS NOT NULL
    DROP TRIGGER trg_UpdateEmpleadoActivo
GO

CREATE TRIGGER trg_UpdateEmpleadoActivo
ON Departamento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE emp
       SET emp.activo = dep.activo
      FROM dbo.empleado emp
           INNER JOIN inserted dep ON emp.idDepartamento = dep.idDepartamento
           INNER JOIN deleted del ON dep.idDepartamento = del.idDepartamento
     WHERE dep.activo <> del.activo;
END;
GO

IF OBJECT_ID('trg_EmpleadoSetActivoFromDepartamento') IS NOT NULL
    DROP TRIGGER dbo.trg_EmpleadoSetActivoFromDepartamento;
GO

CREATE TRIGGER dbo.trg_EmpleadoSetActivoFromDepartamento
ON Empleado
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE emp
       SET emp.activo = dep.activo
      FROM dbo.empleado emp
           INNER JOIN inserted ins ON emp.idEmpleado = ins.idEmpleado
           INNER JOIN dbo.Departamento dep ON ins.idDepartamento = dep.idDepartamento;
END;
GO

IF OBJECT_ID('trg_EmpleadoCambioDepartamento') IS NOT NULL
    DROP TRIGGER trg_EmpleadoCambioDepartamento;
GO

CREATE TRIGGER trg_EmpleadoCambioDepartamento
ON Empleado
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE emp
       SET emp.activo = dep.activo
      FROM dbo.empleado emp
           INNER JOIN inserted ins ON emp.idEmpleado = ins.idEmpleado
           INNER JOIN dbo.Departamento dep ON ins.idDepartamento = dep.idDepartamento
     WHERE ins.idDepartamento <> (SELECT idDepartamento FROM deleted WHERE idEmpleado = ins.idEmpleado);
END;
GO

IF OBJECT_ID('sp_RegistrarError') IS NOT NULL
    DROP PROCEDURE dbo.sp_RegistrarError;
GO

CREATE PROCEDURE dbo.sp_RegistrarError
(
 @mensajeError  NVARCHAR(MAX),
 @lineaError    INT,
 @procedimiento VARCHAR(4000)
)
AS
BEGIN
    INSERT INTO dbo.logErrores (
                                mensajeError,
                                lineaError,
                                procedimiento
                               )
    SELECT @mensajeError,
           @lineaError,
           @procedimiento;
END;
GO



IF OBJECT_ID('sp_InsertarDepartamento') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertarDepartamento;
GO
CREATE PROCEDURE dbo.sp_InsertarDepartamento
(
 @nombre VARCHAR(400),
 @activo BIT
)
AS
BEGIN
    BEGIN TRY
        IF @nombre IS NULL OR LTRIM(RTRIM(@nombre)) = ''
        BEGIN
            THROW 50001, 'El nombre del departamento no puede estar vacio o nulo.', 1;
        END

        BEGIN TRANSACTION;

        INSERT INTO dbo.Departamento
        (
         nombre,
         activo
        )
        SELECT @nombre,
               @activo;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;

        DECLARE @mensaje NVARCHAR(MAX) = ERROR_MESSAGE(),
                @linea   INT = ERROR_LINE(),
                @proc    VARCHAR(4000) = ERROR_PROCEDURE();

        EXECUTE dbo.sp_RegistrarError @mensaje,
                                     @linea,
                                     @proc;

        THROW 50001, 'Error al insertar el departamento.', 1;
    END CATCH
END;
GO

IF OBJECT_ID('sp_ActualizarDepartamento') IS NOT NULL
    DROP PROCEDURE dbo.sp_ActualizarDepartamento;
GO

CREATE PROCEDURE dbo.sp_ActualizarDepartamento
(
 @idDepartamento INTEGER,
 @nombre         VARCHAR(400),
 @activo         BIT
)
AS
BEGIN
    BEGIN TRY
        IF @nombre IS NULL OR LTRIM(RTRIM(@nombre)) = ''
        BEGIN
            THROW 50001, 'El nombre del departamento no puede estar vacio o nulo.', 1;
        END

        IF NOT EXISTS (
                       SELECT 1
                         FROM dbo.Departamento
                        WHERE idDepartamento = @idDepartamento
                      )
        BEGIN
            THROW 50002, 'No se encontro el departamento a actualizar.', 1;
        END

        BEGIN TRANSACTION;

        UPDATE dbo.Departamento
           SET nombre = @nombre,
               activo = @activo
         WHERE idDepartamento = @idDepartamento

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;

        DECLARE @mensaje NVARCHAR(MAX) = ERROR_MESSAGE(),
                @linea   INT = ERROR_LINE(),
                @proc    VARCHAR(4000) = ERROR_PROCEDURE();

        EXECUTE dbo.sp_RegistrarError @mensaje,
                                     @linea,
                                     @proc;

        THROW 50001, 'Error al insertar o actualizar el departamento.', 1;
    END CATCH
END;
GO

IF OBJECT_ID('sp_InsertarEmpleado') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertarEmpleado;
GO
CREATE PROCEDURE dbo.sp_InsertarEmpleado
(
 @nombres         VARCHAR(80),
 @apellidos       VARCHAR(80),
 @fechaNacimiento DATE,
 @sexo            BIT,
 @activo          BIT,
 @fechaIngreso    DATETIME,
 @direccion       VARCHAR(400),
 @nit             VARCHAR(20),
 @dpi             BIGINT,
 @idDepartamento  INT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.Empleado
        (
         nombres,
         apellidos,
         fechaNacimiento,
         sexo,
         fechaIngreso,
         direccion,
         nit,
         dpi,
         idDepartamento,
         activo
        )
        SELECT @nombres,
               @apellidos,
               @fechaNacimiento,
               @sexo,
               @fechaIngreso,
               @direccion,
               @nit,
               @dpi,
               @idDepartamento,
               @activo;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;

        DECLARE @mensaje NVARCHAR(MAX) = ERROR_MESSAGE(),
                @linea INT = ERROR_LINE(),
                @proc VARCHAR(4000) = ERROR_PROCEDURE();

        EXECUTE dbo.sp_RegistrarError @mensaje,
                                     @linea,
                                     @proc;

        THROW 50001, 'Error al insertar el empleado.', 1;
    END CATCH
END;
GO

IF OBJECT_ID('sp_ActualizarEmpleado') IS NOT NULL
    DROP PROCEDURE dbo.sp_ActualizarEmpleado;
GO

CREATE PROCEDURE dbo.sp_ActualizarEmpleado
(
 @idEmpleado      INTEGER,
 @nombres         VARCHAR(80),
 @apellidos       VARCHAR(80),
 @fechaNacimiento DATE,
 @sexo            BIT,
 @activo          BIT,
 @fechaIngreso    DATETIME,
 @direccion       VARCHAR(400),
 @nit             VARCHAR(20),
 @dpi             BIGINT,
 @idDepartamento  INT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (
                       SELECT 1
                         FROM dbo.Empleado
                        WHERE idEmpleado = @idEmpleado
                      )
        BEGIN
            THROW 50003, 'No se encontro el empleado a actualizar.', 1;
        END

        UPDATE dbo.Empleado
           SET nombres          = @nombres,
               apellidos        = @apellidos,
               fechaNacimiento  = @fechaNacimiento,
               sexo             = @sexo,
               fechaIngreso     = @fechaIngreso,
               direccion        = @direccion,
               nit              = @nit,
               dpi              = @dpi,
               idDepartamento   = @idDepartamento,
               activo           = @activo
         WHERE idEmpleado = @idEmpleado;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;

        DECLARE @mensaje NVARCHAR(MAX) = ERROR_MESSAGE(),
                @linea INT = ERROR_LINE(),
                @proc VARCHAR(4000) = ERROR_PROCEDURE();

        EXECUTE dbo.sp_RegistrarError @mensaje,
                                     @linea,
                                     @proc;

        THROW 50001, 'Error al actualizar el empleado.', 1;
    END CATCH
END;
GO

IF OBJECT_ID('fn_ReportarEmpleados') IS NOT NULL
    DROP FUNCTION dbo.fn_ReportarEmpleados;
GO

CREATE FUNCTION dbo.fn_ReportarEmpleados()
RETURNS TABLE
AS
RETURN
    SELECT emp.idEmpleado,
           emp.nombres,
           emp.apellidos,
           DATEDIFF(YEAR, emp.fechaNacimiento, GETDATE()) AS edad,
           emp.activo,
           emp.fechaIngreso,
           dep.nombre departamento
      FROM dbo.empleado emp
           LEFT JOIN dbo.departamento dep ON emp.idDepartamento = dep.idDepartamento;
GO

IF OBJECT_ID('fn_BuscarEmpleado') IS NOT NULL
    DROP FUNCTION dbo.fn_BuscarEmpleado;
GO

CREATE FUNCTION dbo.fn_BuscarEmpleado
(
 @dato NVARCHAR(80)
)
RETURNS TABLE
AS
RETURN
    SELECT emp.idEmpleado,
           emp.nombres,
           emp.apellidos,
           emp.fechaNacimiento,
           emp.sexo,
           emp.activo,
           emp.fechaIngreso,
           emp.direccion,
           emp.nit,
           emp.dpi,
           dep.idDepartamento
      FROM dbo.empleado emp
           LEFT JOIN dbo.departamento dep ON emp.idDepartamento = dep.idDepartamento
     WHERE emp.nit = @dato
        OR CAST(emp.dpi AS NVARCHAR(20)) = @dato
        OR @dato = ''
        OR nombres LIKE '%' + @dato + '%'
        OR apellidos LIKE '%' + @dato + '%'
GO

IF OBJECT_ID('fn_BuscarEmpleadoPorId') IS NOT NULL
    DROP FUNCTION dbo.fn_BuscarEmpleadoPorId;
GO

CREATE FUNCTION dbo.fn_BuscarEmpleadoPorId
(
 @idEmpleado INTEGER
)
RETURNS TABLE
AS
RETURN
    SELECT emp.idEmpleado,
           emp.nombres,
           emp.apellidos,
           emp.fechaNacimiento,
           emp.sexo,
           emp.activo,
           emp.fechaIngreso,
           emp.direccion,
           emp.nit,
           emp.dpi,
           dep.idDepartamento
      FROM dbo.empleado emp
           LEFT JOIN dbo.departamento dep ON emp.idDepartamento = dep.idDepartamento
     WHERE emp.idEmpleado = @idEmpleado
GO

IF OBJECT_ID('fn_BuscarDepartamento') IS NOT NULL
    DROP FUNCTION dbo.fn_BuscarDepartamento;
GO

CREATE FUNCTION dbo.fn_BuscarDepartamento
(
 @dato VARCHAR(400)
)
RETURNS TABLE
AS
RETURN
    SELECT dep.idDepartamento,
           dep.nombre,
           dep.activo
      FROM dbo.departamento dep
     WHERE LTRIM(RTRIM(dep.nombre)) = @dato
        OR LTRIM(RTRIM(CAST(dep.idDepartamento AS VARCHAR(400)))) = LTRIM(RTRIM(@dato))
        OR @dato = ''
        OR nombre LIKE '%' + @dato + '%';
GO

IF OBJECT_ID('fn_BuscarDepartamentoPorId') IS NOT NULL
    DROP FUNCTION dbo.fn_BuscarDepartamentoPorId;
GO

CREATE FUNCTION dbo.fn_BuscarDepartamentoPorId
(
 @idDepartamento INTEGER
)
RETURNS TABLE
AS
RETURN
    SELECT dep.idDepartamento,
           dep.nombre,
           dep.activo
      FROM dbo.departamento dep
     WHERE dep.idDepartamento = @idDepartamento
GO




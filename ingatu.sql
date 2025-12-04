-- 1. CREACIÓN DE LA BASE DE DATOS
CREATE DATABASE IF NOT EXISTS VENTAS;
USE VENTAS;

-- 2. CREACIÓN DE TABLAS
-- CONTROL DE ACCESO AL SISTEMA (2 TIPOS DE USUARIOS)
CREATE TABLE TipoUsuario (
    ID_TipoUsuario INT PRIMARY KEY AUTO_INCREMENT,
    NombreTipo VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Usuario (
    ID_Usuario INT PRIMARY KEY AUTO_INCREMENT,
    NombreUsuario VARCHAR(100) NOT NULL UNIQUE,
    -- NOTA: En producción, se debe usar HASHING (ej. SHA256) en lugar de texto plano
    Contrasena VARCHAR(256) NOT NULL,
    ID_TipoUsuario INT NOT NULL,
    Activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (ID_TipoUsuario) REFERENCES TipoUsuario(ID_TipoUsuario)
);

-- CRUD DE EMPLEADOS
CREATE TABLE Empleado (
    ID_Empleado INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    ApellidoPaterno VARCHAR(100) NOT NULL,
    ApellidoMaterno VARCHAR(100),
    RFC VARCHAR(13) UNIQUE,
    FechaContratacion DATE DEFAULT (CURRENT_DATE()),
    ID_Usuario INT UNIQUE,
    FOREIGN KEY (ID_Usuario) REFERENCES Usuario(ID_Usuario)
);

-- CRUD DE PRODUCTOS
CREATE TABLE Producto (
    CLAVE VARCHAR(20) PRIMARY KEY,
    NOMBRE VARCHAR(100) NOT NULL,
    DESCRIPCION VARCHAR(255),
    PRECIO DECIMAL(10, 2) NOT NULL CHECK (PRECIO >= 0),
    STOCK INT NOT NULL CHECK (STOCK >= 0),
    DESCONTINUADO BOOLEAN not null default (false)
);

-- REGISTRO DE VENTAS (transacciones)
CREATE TABLE Venta (
    ID_Venta INT PRIMARY KEY AUTO_INCREMENT,
    FechaHoraVenta DATETIME DEFAULT (NOW()),
    ID_Empleado INT NOT NULL,
    Total DECIMAL(10, 2) NOT NULL,
    -- c) REPORTES DE VENTAS (PENDIENTES)
    Estado VARCHAR(50) NOT NULL DEFAULT 'Pagada', -- Valores: 'Pagada', 'Pendiente', 'Cancelada'
    FOREIGN KEY (ID_Empleado) REFERENCES Empleado(ID_Empleado)
);

CREATE TABLE DetalleVenta (
    ID_DetalleVenta INT PRIMARY KEY AUTO_INCREMENT,
    ID_Venta INT NOT NULL,
    CLAVE_Producto VARCHAR(20) NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ID_Venta) REFERENCES Venta(ID_Venta),
    FOREIGN KEY (CLAVE_Producto) REFERENCES Producto(CLAVE)
);

-- MONITOREO DE PRODUCTOS (Tabla de auditoría)
CREATE TABLE AuditoriaProducto (
    ID_Auditoria INT PRIMARY KEY AUTO_INCREMENT,
    CLAVE_Producto VARCHAR(20) NOT NULL,
    FechaHoraCambio DATETIME DEFAULT (NOW()),
    TipoCambio VARCHAR(10) NOT NULL, -- INSERT, UPDATE, DELETE
    CampoModificado VARCHAR(50) DEFAULT NULL,
    ValorAnterior TEXT,
    ValorNuevo TEXT,
    UsuarioDB VARCHAR(100) DEFAULT (USER()) -- Usuario de la base de datos en MySQL
);

-- -------------------------------------------------------------
-- 					PROCESOS ALMACENADOS 					  --
-- -------------------------------------------------------------

DELIMITER $$
-- berificar credenciales y obtener el tipo de usuario (para la aplicación)
CREATE PROCEDURE sp_AutenticarUsuario (
    IN p_NombreUsuario VARCHAR(100),
    IN p_Contrasena VARCHAR(256)
)
BEGIN
    SELECT
        U.ID_Usuario,
        U.NombreUsuario,
        T.NombreTipo
    FROM Usuario U
    INNER JOIN TipoUsuario T ON U.ID_TipoUsuario = T.ID_TipoUsuario
    WHERE U.NombreUsuario = p_NombreUsuario
      AND U.Contrasena = p_Contrasena -- Se debe reemplazar por una comparación de hash
      AND U.Activo = TRUE;
END $$

-- Simulación de verificación de permiso (La aplicación debe llamarlo antes de mostrar el CRUD de empleados)
CREATE PROCEDURE sp_VerificarPermiso (
    IN p_ID_Usuario INT,
    IN p_PermisoNecesario VARCHAR(50) -- Ej: 'ADMIN', 'VENTA'
)
BEGIN
    SELECT
        CASE
            WHEN T.NombreTipo = 'Administrador' THEN TRUE
            WHEN T.NombreTipo = 'Vendedor' AND p_PermisoNecesario = 'VENTA' THEN TRUE
            WHEN T.NombreTipo = 'Vendedor' AND p_PermisoNecesario = 'LISTAR_PRODUCTOS' THEN TRUE
            ELSE FALSE
        END AS TienePermiso
    FROM Usuario U
    INNER JOIN TipoUsuario T ON U.ID_TipoUsuario = T.ID_TipoUsuario
    WHERE U.ID_Usuario = p_ID_Usuario;
END $$
DELIMITER ;

DELIMITER $$
-- INSERTAR EMPLEADO
CREATE PROCEDURE sp_InsertarEmpleado (
    IN p_Nombre VARCHAR(100),
    IN p_ApellidoPaterno VARCHAR(100),
    IN p_ApellidoMaterno VARCHAR(100),
    IN p_RFC VARCHAR(13),
    IN p_ID_Usuario INT
)
BEGIN
    -- Validar si el RFC ya existe
    IF p_RFC IS NOT NULL AND EXISTS (SELECT 1 FROM Empleado WHERE RFC = p_RFC) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El RFC proporcionado ya está registrado.';
    ELSE
        INSERT INTO Empleado (Nombre, ApellidoPaterno, ApellidoMaterno, RFC, ID_Usuario)
        VALUES (p_Nombre, p_ApellidoPaterno, p_ApellidoMaterno, p_RFC, p_ID_Usuario);
    END IF;
END $$


--  ACTUALIZAR EMPLEADO
CREATE PROCEDURE sp_ActualizarEmpleado (
    IN p_ID_Empleado INT,
    IN p_Nombre VARCHAR(100),
    IN p_ApellidoPaterno VARCHAR(100),
    IN p_ApellidoMaterno VARCHAR(100),
    IN p_RFC VARCHAR(13),
    IN p_ID_Usuario INT
)
BEGIN
    -- Validar si el RFC ya existe en otro empleado
    IF p_RFC IS NOT NULL AND EXISTS (SELECT 1 FROM Empleado WHERE RFC = p_RFC AND ID_Empleado <> p_ID_Empleado) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El RFC proporcionado ya está registrado en otro empleado.';
    ELSE
        UPDATE Empleado
        SET
            Nombre = p_Nombre,
            ApellidoPaterno = p_ApellidoPaterno,
            ApellidoMaterno = p_ApellidoMaterno,
            RFC = p_RFC,
            ID_Usuario = p_ID_Usuario
        WHERE ID_Empleado = p_ID_Empleado;
    END IF;
END $$


-- ELIMINAR EMPLEADO
CREATE PROCEDURE sp_EliminarEmpleado (
    IN p_ID_Empleado INT
)
BEGIN
    DELETE FROM Empleado WHERE ID_Empleado = p_ID_Empleado;
END $$


-- LISTAR EMPLEADOS
CREATE PROCEDURE sp_ListarEmpleados ()
BEGIN
    SELECT * FROM Empleado;
END $$

DELIMITER ;

-- -------------------------------------------------------------
-- 						FUNCIONES		 					  --
-- -------------------------------------------------------------

DELIMITER $$
-- Función para calcular el total de ventas en un rango de fechas
CREATE FUNCTION fn_TotalVentasPorRango
(
    FechaInicio DATE,
    FechaFin DATE
)
RETURNS DECIMAL(10, 2)
READS SQL DATA
BEGIN
    DECLARE Total DECIMAL(10, 2);

    SELECT SUM(Total) INTO Total
    FROM Venta
    WHERE FechaHoraVenta >= FechaInicio AND FechaHoraVenta < DATE_ADD(FechaFin, INTERVAL 1 DAY);

    RETURN IFNULL(Total, 0);
END $$
DELIMITER ;


-- -------------------------------------------------------------
-- 							TRIGERS		 					  --
-- -------------------------------------------------------------

DELIMITER $$
CREATE TRIGGER tr_AuditoriaProducto
AFTER INSERT ON Producto
FOR EACH ROW
BEGIN
    INSERT INTO AuditoriaProducto (CLAVE_Producto, TipoCambio, ValorNuevo)
    VALUES (
        NEW.CLAVE,
        'INSERT',
        CONCAT('CLAVE: ', NEW.CLAVE, ', NOMBRE: ', NEW.NOMBRE, ', PRECIO: ', NEW.PRECIO, ', STOCK: ', NEW.STOCK)
    );
END $$

CREATE TRIGGER tr_AuditoriaProducto_Update
AFTER UPDATE ON Producto
FOR EACH ROW
BEGIN
    IF OLD.NOMBRE <> NEW.NOMBRE OR OLD.DESCRIPCION <> NEW.DESCRIPCION OR OLD.PRECIO <> NEW.PRECIO OR OLD.STOCK <> NEW.STOCK THEN
        INSERT INTO AuditoriaProducto (CLAVE_Producto, TipoCambio, CampoModificado, ValorAnterior, ValorNuevo)
        VALUES (
            NEW.CLAVE,
            'UPDATE',
            'General',
            CONCAT('NOMBRE: ', OLD.NOMBRE, ', PRECIO: ', OLD.PRECIO, ', STOCK: ', OLD.STOCK),
            CONCAT('NOMBRE: ', NEW.NOMBRE, ', PRECIO: ', NEW.PRECIO, ', STOCK: ', NEW.STOCK)
        );
    END IF;
END $$

CREATE TRIGGER tr_AuditoriaProducto_Delete
AFTER DELETE ON Producto
FOR EACH ROW
BEGIN
    INSERT INTO AuditoriaProducto (CLAVE_Producto, TipoCambio, ValorAnterior)
    VALUES (
        OLD.CLAVE,
        'DELETE',
        CONCAT('CLAVE: ', OLD.CLAVE, ', NOMBRE: ', OLD.NOMBRE, ', PRECIO: ', OLD.PRECIO, ', STOCK: ', OLD.STOCK)
    );
END $$
DELIMITER ;

-- -------------------------------------------------------------
-- 						   REGISTROS   	 				     --
-- -------------------------------------------------------------
USE VENTAS;

-- TIPOS DE USUARIO
-- Se asume que estos ya existen: ID 1 = Administrador, ID 2 = Vendedor
INSERT INTO TipoUsuario (ID_TipoUsuario, NombreTipo) VALUES (1, 'Administrador') ON DUPLICATE KEY UPDATE NombreTipo='Administrador';
INSERT INTO TipoUsuario (ID_TipoUsuario, NombreTipo) VALUES (2, 'Vendedor') ON DUPLICATE KEY UPDATE NombreTipo='Vendedor';

-- USUARIOS (3 Registros)
-- Usuario 1: Administrador
INSERT INTO Usuario (NombreUsuario, Contrasena, ID_TipoUsuario) VALUES ('admin', 'pass123', 1);
-- Usuario 2: Vendedor (Juan)
INSERT INTO Usuario (NombreUsuario, Contrasena, ID_TipoUsuario) VALUES ('juan.v', 'pass123', 2);
-- Usuario 3: Vendedor (Ana)
INSERT INTO Usuario (NombreUsuario, Contrasena, ID_TipoUsuario) VALUES ('ana.g', 'pass123', 2);

-- EMPLEADOS (3 Registros)
-- Usamos los Stored Procedures para demostrar su uso
CALL sp_InsertarEmpleado('Juan', 'Pérez', 'López', 'PEHJ750101', 2); -- ID_Empleado = 1 (Asociado a juan.v)
CALL sp_InsertarEmpleado('Ana', 'García', 'Ruiz', 'GARA801212', 3);   -- ID_Empleado = 2 (Asociado a ana.g)
CALL sp_InsertarEmpleado('María', 'Sánchez', 'Díaz', 'SADM900303', 1); -- ID_Empleado = 3 (Asociado a admin)

-- PRODUCTOS (10 Registros de prueba)
-- NOTA: La inserción de estos productos automáticamente gatillará el TRIGGER de auditoría.
INSERT INTO Producto (CLAVE, NOMBRE, DESCRIPCION, PRECIO, STOCK) VALUES
('P001', 'Laptop Gamer', 'Portátil de alto rendimiento', 1500.00, 10),
('P002', 'Monitor 27"', 'Monitor LED Full HD', 250.50, 25),
('P003', 'Teclado Mecánico', 'Teclado con switches marrones', 75.99, 50),
('P004', 'Mouse Inalámbrico', 'Mouse óptico ergonómico', 25.00, 100),
('P005', 'SSD 1TB', 'Unidad de estado sólido de 1TB', 89.99, 30),
('P006', 'Memoria RAM 16GB', 'Módulo DDR4 3200MHz', 65.00, 40),
('P007', 'Disco Duro Externo 2TB', 'Almacenamiento portátil USB 3.0', 79.50, 15),
('P008', 'Impresora Láser', 'Impresora monocromática', 120.00, 5),
('P009', 'Webcam HD', 'Cámara web con micrófono', 35.00, 60),
('P010', 'Router WiFi 6', 'Router de doble banda de última generación', 99.00, 20);

-- REGISTRO DE VENTAS (3 Transacciones)

-- Venta 1: Pagada, Empleado Juan Pérez (ID 1)
INSERT INTO Venta (ID_Empleado, Total, Estado) VALUES (1, 1575.99, 'Pagada');
SET @last_venta_id = LAST_INSERT_ID();
INSERT INTO DetalleVenta (ID_Venta, CLAVE_Producto, Cantidad, PrecioUnitario, Subtotal) VALUES 
(@last_venta_id, 'P001', 1, 1500.00, 1500.00),
(@last_venta_id, 'P003', 1, 75.99, 75.99);

-- Venta 2: Pendiente, Empleado Ana García (ID 2)
INSERT INTO Venta (ID_Empleado, Total, Estado) VALUES (2, 501.00, 'Pendiente');
SET @last_venta_id = LAST_INSERT_ID();
INSERT INTO DetalleVenta (ID_Venta, CLAVE_Producto, Cantidad, PrecioUnitario, Subtotal) VALUES 
(@last_venta_id, 'P002', 2, 250.50, 501.00);

-- Venta 3: Pagada, Empleado Juan Pérez (ID 1)
INSERT INTO Venta (ID_Empleado, Total, Estado) VALUES (1, 114.99, 'Pagada');
SET @last_venta_id = LAST_INSERT_ID();
INSERT INTO DetalleVenta (ID_Venta, CLAVE_Producto, Cantidad, PrecioUnitario, Subtotal) VALUES 
(@last_venta_id, 'P005', 1, 89.99, 89.99),
(@last_venta_id, 'P004', 1, 25.00, 25.00);

select * from usuario;


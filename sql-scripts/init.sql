-- Crear base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CATALOGO_P3_DB')
BEGIN
    CREATE DATABASE CATALOGO_P3_DB
END
GO

USE CATALOGO_P3_DB
GO

-- Tabla MARCAS
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MARCAS')
BEGIN
    CREATE TABLE MARCAS (
        Id INT IDENTITY(1,1) NOT NULL,
        Descripcion VARCHAR(50) NULL,
        PRIMARY KEY (Id)
    )
END
GO

-- Tabla CATEGORIAS
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CATEGORIAS')
BEGIN
    CREATE TABLE CATEGORIAS (
        Id INT IDENTITY(1,1) NOT NULL,
        Descripcion VARCHAR(50) NULL,
        PRIMARY KEY (Id)
    )
END
GO

-- Tabla ARTICULOS
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ARTICULOS')
BEGIN
    CREATE TABLE ARTICULOS (
        Id INT IDENTITY(1,1) NOT NULL,
        Codigo VARCHAR(50) NULL,
        Nombre VARCHAR(50) NULL,
        Descripcion VARCHAR(150) NULL,
        IdMarca INT NULL,
        IdCategoria INT NULL,
        Precio MONEY NULL,
        PRIMARY KEY (Id)
    )
END
GO

-- Tabla IMAGENES
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'IMAGENES')
BEGIN
    CREATE TABLE IMAGENES (
        Id INT IDENTITY(1,1) NOT NULL,
        IdArticulo INT NOT NULL,
        ImagenUrl VARCHAR(1000) NOT NULL
    )
END
GO

-- Insertar Marcas
IF NOT EXISTS (SELECT * FROM MARCAS)
BEGIN
    INSERT INTO MARCAS VALUES ('Samsung'), ('Apple'), ('Sony'), ('Huawei'), ('Motorola')
END
GO

-- Insertar Categorias
IF NOT EXISTS (SELECT * FROM CATEGORIAS)
BEGIN
    INSERT INTO CATEGORIAS VALUES ('Celulares'), ('Televisores'), ('Media'), ('Audio')
END
GO

-- Insertar Articulos
IF NOT EXISTS (SELECT * FROM ARTICULOS)
BEGIN
    INSERT INTO ARTICULOS VALUES 
    ('S01', 'Galaxy S10', 'Una canoa cara', 1, 1, 69.999),
    ('M03', 'Moto G Play 7ma Gen', 'Ya siete de estos?', 1, 5, 15699),
    ('S99', 'Play 4', 'Ya no se cuantas versiones hay', 3, 3, 35000),
    ('S56', 'Bravia 55', 'Alta tele', 3, 2, 49500),
    ('A23', 'Apple TV', 'lindo loro', 2, 3, 7850)
END
GO

-- Insertar Imagenes
IF NOT EXISTS (SELECT * FROM IMAGENES)
BEGIN
    INSERT INTO IMAGENES VALUES
    (1, 'https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542'),
    (2, 'https://www.motorola.cl/arquivos/moto-g7-play-img-product.png?v=636862863804700000'),
    (2, 'https://i.blogs.es/9da288/moto-g7-/1366_2000.jpg'),
    (3, 'https://www.euronics.cz/image/product/800x800/532620.jpg'),
    (4, 'https://intercompras.com/product_thumb_keepratio_2.php?img=images/product/SONY_KDL-55W950A.jpg&w=650&h=450'),
    (5, 'https://cnnespanol2.files.wordpress.com/2015/12/gadgets-mc3a1s-populares-apple-tv-2015-18.jpg?quality=100&strip=info&w=460&h=260&crop=1')
END
GO
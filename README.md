
# Consigna TP WinForm
Requisitos de finalización
Se necesita una aplicación para la gestión de artículos de un catálogo de un comercio. La aplicación debe ser genérica, es decir, aplicar para cualquier tipo de comercio; y la información que en ella se cargue será consumida luego desde distintos servicios para ser mostradas ya sea en webs, e-commerces, apps mobile, revistas, etc. Esto no es parte del desarrollo, pero sí del contexto en el cual se utilizará la aplicación a desarrollar.
Deberá ser un programa de escritorio que contemple la administración de artículos. Las funcionalidades que deberá tener la aplicación serán:
•	Listado de artículos.
•	Búsqueda de artículos por distintos criterios.
•	Agregar artículos.
•	Modificar artículos.
•	Eliminar artículos.
•	Ver detalle de un artículo.
Toda ésta información deberá ser persistida en una base de datos ya existente (la cual se adjunta).
Los datos mínimos con los que deberá contar el artículo son los siguientes:
•	Código de artículo.
•	Nombre.
•	Descripción.
•	Marca (seleccionable de una lista desplegable).
•	Categoría (seleccionable de una lista desplegable.
•	Imagen.
•	Precio.
El programa debe permitir administrar las Marcas y Categorías disponibles en el programa. Además, un producto podría llegar a tener una o más imágenes, sin un límite establecido. Esto debe estar contemplado en la gestión del artículo.
Etapa 1: Construir las clases necesarias para el modelo de dicha aplicación junto a las ventanas con las que contará y su navegación.
Etapa 2: Construir la interacción con la base de datos y validaciones correspondiente para dar vida a la funcionalidad.

---

## Estado de Implementación (Etapa 1 y 2)

### Dominio (Capa de entidades) ✅
- `Articulo.cs` - Entidad completa con todas las propiedades
- `Categoria.cs` - Entidad básica (Id, Descripcion)
- `Marca.cs` - Entidad básica (Id, Descripcion)
- `Imagen.cs` - Entidad para URLs de imágenes

### Negocio (Capa de datos y lógica) ✅
- `AccesoDatos.cs` - Conexión a SQL Server mediante App.config
- `ArticuloNegocio.cs` - CRUD completo:
  - `Listar()` - Lista todos los artículos con JOIN a Marca y Categoría
  - `Agregar(Articulo)` - Crea nuevo artículo en BD
  - `Modificar(Articulo)` - Actualiza artículo existente
  - `Eliminar(int id)` - Elimina artículo por ID
  - `Buscar(string criterio, string texto)` - Búsqueda por Código/Nombre/Marca

### Presentacion (Capa UI) ✅
- `frmPrincipal.cs` - Formulario principal con:
  - DataGridView que muestra artículos
  - Botones: Agregar, Modificar, Eliminar, Ver Detalle
  - Búsqueda por nombre y rango de precios (filtros en memoria)
- `frmArticulo.cs` - Formulario ABM de artículos:
  - Datos: Código, Nombre, Descripción, Precio
  - ComboBox: Marca (hardcodeado)
  - ComboBox: Categoría (hardcodeado)
  - Gestión de múltiples imágenes (agregar/eliminar)
- `frmDetalle.cs` - Vista de detalle de artículo

### Base de Datos ✅
- `sql-scripts/init.sql` - Script completo con:
  - Creación de tablas (ARTICULOS, MARCAS, CATEGORIAS, IMAGENES)
  - Datos de prueba insertados

#### Cómo levantar la base de datos

El proyecto usa Docker con SQL Server en contenedor.

**Iniciar el contenedor**
```bash
# Desde la raíz del proyecto
docker-compose up -d
```

**Ejecutar el script de inicialización**
```bash
# Una vez que el contenedor esté corriendo
docker exec -i sqlserver sqlcmd -S localhost -U SA -P "TuPassword123!" -i /sql-scripts/init.sql
```

**Verificar que está creada**
```bash
docker exec -i sqlserver sqlcmd -S localhost -U SA -P "TuPassword123!" -Q "USE CATALOGO_P3_DB; SELECT * FROM ARTICULOS;"
```

Si devuelve 5 artículos, la base está lista.

**Detener el contenedor**
```bash
docker-compose down
```

---

## Pendiente
- **Conectar Presentación con Negocio**: Los formularios actualmente usan datos hardcodeados en memoria. Falta reemplazar las llamadas a la lista local por los métodos de `ArticuloNegocio` para utilizar la base de datos real.

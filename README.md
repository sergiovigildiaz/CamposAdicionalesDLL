# Campos Adicionales - a3ERP

Este proyecto consiste en una librería dinámica (DLL) llamada **CamposAdicionales** que se integra con el software **a3ERP**. Permite a los usuarios guardar valores en hasta 20 campos adicionales en las tablas de **Clientes**, **Proveedores**, y **Artículos** de la base de datos de a3ERP.

## Tabla de Contenidos
- [Campos Disponibles](#campos-disponibles)
- [Funcionalidades Principales](#funcionalidades-principales)
- [Métodos Clave](#métodos-clave)
  - [ActualizarCampo()](#actualizarcampo)
- [Cargar y Guardar Campos](#cargar-y-guardar-campos)
  - [CargarCampos()](#cargarcampos)
  - [AsignarColumnas()](#asignarcolumnas)
- [Interfaz del Formulario](#interfaz-del-formulario)
- [Uso](#uso)

## Campos Disponibles

Los campos adicionales se han dividido de la siguiente manera:

- **VARCHAR**: ADI1 a ADI10
- **DECIMAL**: ADI11 a ADI14
- **DATETIME**: ADI15 a ADI18
- **TEXT**: ADI19
- **IMAGE**: ADI20

## Funcionalidades Principales

1. **Conexión con a3ERP**: La DLL establece una conexión con la base de datos y con el sistema ERP.
2. **Gestión de Formularios**: La clase gestiona un formulario que permite a los usuarios visualizar y editar los 20 campos adicionales. 
3. **Eventos de Documentos**: Implementa métodos para gestionar eventos antes y después de guardar o cargar documentos en el sistema ERP.

## Métodos Clave

### `ActualizarCampo()`

Este método se utiliza para actualizar los valores en las tablas de la base de datos (**__CLIENTES**, **__PROVEED**, **ARTICULO**). Los parámetros son:

- **nombreCampo**: El campo ADI a modificar.
- **valorCampo**: El valor a guardar en el campo.
- **código**: El código del cliente, proveedor o artículo.
- **nombreTabla**: La tabla correspondiente (`__CLIENTES`, `__PROVEED`, `ARTICULO`).

Dependiendo del tipo de campo (VARCHAR, DECIMAL, DATETIME, TEXT, IMAGE), el método maneja el tipo de dato correctamente para realizar la actualización.

## Cargar y Guardar Campos

### `CargarCampos()`
Este método carga dinámicamente los controles en el formulario a partir de un archivo `.txt`, que define qué campos se deben mostrar.

### `AsignarColumnas()`
Al guardar, este método asigna los valores introducidos por el usuario a los campos correspondientes (ADI1 a ADI20) y los guarda en la base de datos.

## Interfaz del Formulario

El formulario principal utiliza un **tableLayoutPanel** que contiene:

- **TextBox** para campos VARCHAR
- **NumericUpDown** para campos DECIMAL
- **DateTimePicker** para campos DATETIME
- **RichTextBox** para campos TEXT
- **FlowLayoutPanel** para imágenes (IMAGE)

## Uso

1. **Cargar el formulario**: El sistema carga los campos configurados en el archivo `.txt` y recupera los valores actuales de la base de datos.
2. **Guardar los cambios**: Se actualizan los valores en la base de datos llamando al método `ActualizarCampo()`.
3. **Vaciar los campos**: El formulario incluye la opción de limpiar todos los campos a través del método `BorrarCamposDeInput()`.

---

Este proyecto es una integración modular para el sistema **a3ERP** que permite la gestión de datos adicionales, mejorando así la funcionalidad del ERP.

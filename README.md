# 💬 Chat — WPF, ASP.NET Core & SignalR

Aplicación de chat en tiempo real desarrollada con **C# y .NET**, diseñada con una arquitectura cliente-servidor que separa la interfaz de usuario, la lógica de backend y los modelos compartidos.

El proyecto demuestra conocimientos en desarrollo de aplicaciones de escritorio, construcción de APIs REST, comunicación bidireccional en tiempo real y persistencia de datos con Entity Framework Core.

## 🚀 Características principales

- **Arquitectura cliente-servidor:** separación entre la aplicación de escritorio, la API y los modelos compartidos.
- **API REST:** comunicación entre el cliente y el servidor mediante solicitudes HTTP/HTTPS.
- **Comunicación en tiempo real:** integración de SignalR para intercambiar mensajes y mantener una comunicación bidireccional.
- **Acceso a datos:** integración de Entity Framework Core con SQLite en memoria.
- **Modelos compartidos:** proyecto independiente para centralizar los objetos comunes entre cliente y servidor.
- **Transferencia de datos mediante DTOs:** separación entre las entidades internas y los datos expuestos por los controladores de la API.

## ▶️ Requisitos y ejecución

### Requisitos previos

- Windows, para ejecutar la aplicación WPF.
- .NET SDK compatible con la versión de los proyectos.
- Git, para clonar el repositorio.
- Visual Studio (opcional), con las herramientas de desarrollo de escritorio .NET y ASP.NET instaladas.

### 1\. Clonar el repositorio

```
git clone https://github.com/Gab-1700/Chat.git
cd Chat
```

### 2\. Restaurar y compilar

Desde el directorio de la solución:

```
dotnet restore
dotnet build
```

### 3\. Ejecutar el servidor

En una terminal, inicia la API:

```
dotnet run --project Chat.Servidor/Chat.Servidor.csproj
```

Comprueba la dirección y el puerto en los que está escuchando el servidor.

### 4\. Ejecutar el cliente

En otra terminal, inicia la aplicación WPF:

```
dotnet run --project Chat.Cliente/Chat.Cliente.csproj
```

Asegúrate de que las direcciones configuradas para la API REST y SignalR coincidan con las del servidor.

**Orden de inicio:** primero `Chat.Servidor` y después `Chat.Cliente`.

## 🏗️ Arquitectura del proyecto

El proyecto está organizado en tres componentes principales:

```
Chat.Application/
├── Chat.Base/
│   └── Objetos y modelos compartidos
│
├── Chat.Cliente/
│   └── Aplicación de escritorio WPF
│
└── Chat.Servidor/
    └── API REST con ASP.NET Core
```

### 1\. Chat.Base

Biblioteca compartida que contiene los objetos comunes utilizados por el cliente y el servidor.

**Responsabilidades:**

- Centralizar los modelos y contratos compartidos.
- Facilitar la interoperabilidad entre los componentes.
- Reducir la duplicación de estructuras de datos.

### 2\. Chat.Cliente

Aplicación de escritorio desarrollada con **Windows Presentation Foundation (WPF)**.

**Responsabilidades:**

- Proporcionar la interfaz gráfica para interactuar con el chat.
- Consumir los endpoints de la API mediante solicitudes HTTP/HTTPS.
- Establecer la comunicación en tiempo real mediante SignalR.
- Presentar los mensajes y la información recibida del servidor.

### 3\. Chat.Servidor

API REST desarrollada con **ASP.NET Core**, responsable de gestionar las solicitudes del cliente y coordinar el acceso a los datos.

**Responsabilidades:**

- Exponer endpoints REST para las operaciones del sistema.
- Gestionar la lógica de backend.
- Integrar Entity Framework Core para el acceso a datos.
- Utilizar SQLite en memoria para la persistencia durante la ejecución.
- Administrar la comunicación en tiempo real mediante SignalR.
- Utilizar DTOs para controlar los datos que entran y salen de los endpoints, evitando exponer directamente las entidades de persistencia.

## 🛠️ Tecnologías utilizadas

| Tecnología | Aplicación en el proyecto |
| --- | --- |
| C# | Lenguaje principal |
| .NET | Plataforma de desarrollo |
| WPF | Interfaz de escritorio |
| ASP.NET Core Web API | Servicios REST |
| SignalR | Comunicación en tiempo real |
| Entity Framework Core | Acceso y mapeo de datos |
| SQLite en memoria | Almacenamiento temporal |
| DTOs | Transferencia controlada de datos |
| HTTP/HTTPS | Comunicación cliente-servidor |

## 🔄 Flujo de comunicación

1. El usuario interactúa con la aplicación WPF.
2. El cliente envía solicitudes HTTP/HTTPS a la API REST para las operaciones correspondientes.
3. El servidor procesa las solicitudes y utiliza Entity Framework Core para acceder a los datos.
4. Cuando se necesita comunicación en tiempo real, SignalR permite enviar y recibir mensajes entre los clientes conectados.
5. Los DTOs permiten definir los datos que se exponen al cliente sin acoplar directamente la API a las entidades internas.

## 💡 Decisiones técnicas destacadas

### Comunicación híbrida

El proyecto combina dos mecanismos de comunicación según su propósito:

- **HTTP/HTTPS:** solicitudes y respuestas para operaciones de la API.
- **SignalR:** comunicación bidireccional en tiempo real para los mensajes del chat.

Esta combinación permite separar las operaciones tradicionales del backend de aquellas que necesitan actualizaciones inmediatas.

### Persistencia con SQLite en memoria

La integración de SQLite en memoria permite trabajar con almacenamiento temporal durante la ejecución de la aplicación, facilitando escenarios de desarrollo y pruebas sin requerir una instancia de base de datos persistente.

## 🎯 Objetivos de aprendizaje demostrados

Este proyecto permite apreciar experiencia práctica en:

- Desarrollo de aplicaciones de escritorio con WPF.
- Diseño e implementación de APIs REST.
- Integración de tecnologías de comunicación en tiempo real.
- Acceso a datos mediante un ORM.
- Organización de soluciones .NET en proyectos separados.
- Diseño de contratos de datos mediante DTOs.
- Separación de responsabilidades entre interfaz, servicios y acceso a datos.

## ▶️ Ejecución del proyecto

1. Clona el repositorio.
2. Abre la solución en Visual Studio.
3. Restaura los paquetes NuGet.
4. Configura el servidor y sus direcciones de conexión según el entorno.
5. Inicia `Chat.Servidor`.
6. Ejecuta `Chat.Cliente` y verifica la conexión con la API y SignalR.

> Los pasos exactos pueden variar según la configuración de los proyectos y las versiones de .NET utilizadas.

## 👨‍💻 Sobre el proyecto

Este proyecto forma parte de mi desarrollo profesional en el ecosistema .NET y refleja mi interés por construir aplicaciones mantenibles, con responsabilidades bien definidas y mecanismos de comunicación adecuados para cada necesidad.

**Áreas principales:** desarrollo backend con ASP.NET Core, aplicaciones de escritorio con WPF, diseño de APIs REST y sistemas de comunicación en tiempo real.

---

_Desarrollado con C# y .NET._

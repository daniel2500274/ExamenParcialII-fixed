# Universidad San Pablo de Guatemala 🎓🏫
# Parcial II - Unit Testing y refactorizacion SOLID

## Erick Daniel Ramirez Divas - 2500274 

Este proyecto es una aplicación de consola en C# que calcula el precio total de un carrito de compras. La arquitectura original (una sola clase con múltiples responsabilidades) ha sido refactorizada para implementar de forma práctica los principios de diseño **SOLID**.


## Filosofía de Arquitectura

El diseño se centra en la **Inversión de Dependencias (DIP)** y la **Responsabilidad Única (SRP)**.

1.  **`CartPriceCalculator` (El Orquestador):**
    * La clase principal, `CartPriceCalculator`, actúa como un "orquestador" o "director".
    * Su **única responsabilidad** es coordinar el flujo de trabajo para calcular un total.
    * **No depende** de clases concretas
    * Las dependencias reales le son inyectadas a través de su constructor.

2.  **Abstracciones:**
    * Toda la lógica de negocio y de infraestructura se define a través de interfaces (`ICartValidator`, `IShippingCalculator`, `IEmailSender`, etc.). 

3.  **Principio Abierto/Cerrado (OCP) para Descuentos:**
    * La lógica de descuentos se implementa usando el **Patrón Strategy**.
    * `IDiscountCalculator` usa una `IEnumerable<IDiscountRule>` (una colección de reglas).
    * Para agregar un nuevo tipo de descuento (ej. "CyberMonday"), no necesitamos *modificar* `DiscountCalculator`; solo creamos una nueva clase que implemente `IDiscountRule` y la *agregamos* a la lista.

## Estructura del Proyecto

La estructura de carpetas refleja la separación de responsabilidades (SRP):

<pre style="color: green;">
   ._________________.
   |.---------------.|
   ||      C#       ||
   ||   -._ .-.     ||
   ||   -._| | |    ||
   ||   -._|"|"|    ||
   ||   -._|.-.|    ||
   ||_______________||
   /.-.-.-.-.-.-.-.-.\
  /.-.-.-.-.-.-.-.-.-.\
 /.-.-.-.-.-.-.-.-.-.-.\
/______/__________\___o_\ 
\_______________________/

CartApp/
│
├── CartPriceCalculator.cs  // <-- El Orquestador (depende solo de interfaces)
├── Program.cs              // <-- Punto de entrada de la consola
│
├── 📂 Core/                // <-- Lógica de negocio
│   ├── 📂 Calcs/           // Implementaciones de cálculo
│   │   ├── CartValidator.cs
│   │   ├── FragileSurchargeCalculator.cs
│   │   ├── IVACalculator.cs  (Implementación de IIVACalculator)
│   │   ├── ShippingCalculator.cs
│   │   └── SubtotalCalculator.cs
│   │
│   └── 📂 Discounts/       // Lógica de descuentos 
│       ├── CouponDiscountRule.cs
│       ├── DiscountCalculator.cs
│       └── VipDiscountRule.cs
│
├── 📂 Models/              // <-- Clases de datos
│   ├── DiscountsDTO.cs     // Datos para reglas de descuento
│   ├── Item.cs             // modelo de item del carrito
│   └── ReceiptsDataDTO.cs  // Datos para recibos y emails
│
├── 📂 Services/            // <-- Lógica de Infraestructura 
│   ├── 📂 Loggin/          // Servicio de Logging
│   │   ├── ConsoleLogger.cs
│   │   └── ILogger.cs
│   │
│   └── 📂 Notify/          // Servicios de notificación (Email, Impresión)
│       ├── EmailSender.cs
│       ├── NotificationService.cs
│       ├── ReceiptFormatter.cs
│       └── ReceiptPrinter.cs
│
└── 📂 Settings/            // <-- Lógica de Configuración
    ├── ConfigsReader.cs    // Implementación que lee de StaticConfig
    ├── StaticConfig.cs     // Dependencia concreta de configuración
    └── interfaces/
        └── IConfigsDTO.cs  // Abstracción de la configuración

</pre>

## Flujo de Funcionamiento (`CalculateTotal`)

Cuando se llama al método `CartPriceCalculator.CalculateTotal(...)`, ocurre el siguiente flujo orquestado:

1.  **Log:** `ILogger` registra el inicio.
2.  **Validar:** Se llama a `ICartValidator.Validate()` para comprobar precios negativos.
3.  **Calcular Subtotal:** Se llama a `ISubtotalCalculator.Calculate()`.
4.  **Calcular Descuentos (OCP):**
    * Se crea un `DiscountsDTO` con los datos del cupón y VIP.
    * Se llama a `IDiscountCalculator.Calculate()`.
    * Este, a su vez, itera sobre su `IEnumerable<IDiscountRule>` (que contiene `VipDiscountRule` y `CouponDiscountRule`) y suma los descuentos aplicables.
5.  **Calcular Componentes:** Se llaman `IShippingCalculator`, `ISurchargeCalculator` (para frágil) y `IIVACalculator` usando los subtotales correspondientes.
6.  **Totalizar:** Se suman todos los componentes para obtener el `total`.
7.  **Notificar:**
    * Se crea un `ReceiptsDataDTO` con todos los resultados.
    * Se llama a `INotificationService.PrintConsoleReceipt()`.
    * Si `emailReceipt` es `true`, se llama a `INotificationService.SendEmailReceipt()`.
8.  **Flujo de Notificación (SRP):**
    * `NotificationService` no formatea ni imprime.
    * Llama a `IReceiptFormatter.Format()` para obtener el `string` del recibo.
    * Luego, pasa ese `string` a `IReceiptPrinter.Print()` y/o `IEmailSender.Send()`.
9.  **Log:** `ILogger` registra el fin del cálculo y el total.

## Cómo Ejecutar el Proyecto

### Ejecutar la Aplicación

La aplicación se puede ejecutar directamente. `Program.cs` actuará como el "cliente" que consume el `CartPriceCalculator`:

```bash
dotnet run --project CartApp/CartApp.csproj
```

### Ejecutar las Pruebas
El proyecto CartApp.Tests contiene un conjunto de 13 pruebas unitarias que validan el comportamiento del sistema.
Para ejecutar las pruebas desde la línea de comandos:

```bash
dotnet test
```

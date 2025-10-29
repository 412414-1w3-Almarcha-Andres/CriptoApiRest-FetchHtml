Parcial I /2024
Enunciado:
Se necesita desarrollar una aplicación orientada a servicios (de tipo WebApi)
que permita gestionar las criptomonedas de una cartera digital (wallet).
Para ello Ud. dispone de un proyecto incompleto que deberá completar,
comprimir y adjuntar a la tarea configurada como instancia de evaluación.
Se pide:
*A partir del script cripto.sql tiene creada un base de datos en nuestro servidor SQL y
mediante un proceso de ingeniería inversa se han creado los siguientes modelos:

 CriptoMoneda (Id, Nombre, Simbolo, Categoria, ValorActual, FechaUltimaCotizacion, Habilitada)
 Categoria (Id, Nombre)

Definir una capa de acceso a datos mediante la implementación de un patrón repository que permita:
* consultar criptomonedas de una categoría determinada ("Plataforma", "Moneda", "Token"),
* actualizar el valor actual (junto con la fecha/hora de la última cotización) de una criptomoneda 
identificada por símbolo (por ejemplo: BTC para Bitcoins)
* registrar la inhabilitación de una moneda.

Tener en cuenta algunas consideraciones:
*Al momento de actualizar la cotización de una moneda la fecha/hora de la última cotización no puede ser
superior a un día. Por ejemplo, es posible indicar que el valor de ayer fue de x dólares, 
pero no el de antes de ayer.
*Solo es posible consultar monedas cuya última actualización no supere un día a la fecha.
*Solo es posible registrar la baja (inhabilitación) de una moneda si su estado es “H” (Habilitada). 
Los estados posibles son: “H”-Habilitada | “NH” – No Habilitada.

Desarrollo:
*Crear un controller que permita gestionar los siguientes endpoints (50 pts.):
*GET /cripto | Recupera todas las criptomonedas según las consideraciones indicadas
*PUT /cripto?simbolo=ETC; valorActual=20 |Permite actualizar el valor de la moneda a partir del símbolo.
*DELETE /cripto/1: Permite actualizar el estado de la criptomoneda identificada por id.
*Desarrollar un Repositorio que permita dar soporte al controller anterior con los métodos correspondientes. (50 pts)


Condiciones de entrega:
Para la entrega del proyecto utilizar la siguiente URL correspondiente a un Drive de Google: https://drive.google.com/drive/folders/1nCDXAuDdJr70Bg78Vk5HnUC2E1X7h5uf?usp=sharing
Allí deberá subir el proyecto comprimido en formato .zip o .rar con el siguiente nombre:
<legajo>_<nombreCompleto>.rar/.zip
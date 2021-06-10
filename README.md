# chiang.desarrollador

Una plataforma de Red Social, permite las siguientes operaciones a sus usuarios: post, follows, re-post
La plataforma provee a los desarrolladores de aplicaciones, el siguiente API:
 
GET /<username>/followers
    
{ “user”: “username”,  “Followers”: [“user1”, “user2”,….. “user n”] }

    
GET /<username>/following
    
{ “user”: “username”,  “Following”: [“user1”, “user2”,….. “user n”] }
    
 Implemente un algoritmo en cualquier lenguaje de programación, que calcule la distancia entre 2 usuarios.
Ejemplo:

Dado:
    
{ “user”: “userA”,  “Following”: [“userB”, “userD”,“userE”, "userG"] }
    
{ “user”: “userB”,  “Following”: [“userC”, “userJ”,“userI”, "userE"] }
    
{ “user”: “userC”,  “Following”: [“userM”, “userN”,“userJ”, "userI", "userE"] }

SI requiero a distancia entre "userA" y "userM"
Al buscar se encuentra que: User A, sigue a User B. Y User B, sigue a User C. Y User C, sigue User M
Entonces, la distancia entre User A y User M, es: 3

# chiang.desarrollador

La solución fue desarrollada en dos capas, donde en una se maneja la interfaz, que pueda consumir un sistema externo , y la otra es un proyecto de tipo libreria que maneja las entidades involucradas dentro de la solución propuesta, dentro del proceso de analsis para poder realizar el analisis de la distancia entre usuarios, primero se realiza un proceso de prerequisitos, entre ello: si el formato de los `follows` es correcto , si existe informacion de los `follows`, si el usuario inicial o final estan vacios, si el usuario inicial o final existen dentro de los `follows` ; unas ves cumplido todos y cada una ellas el proceso de recorrido comienza caso contrario, devolvera obervaciones por cada una de ellas dependa como sea el escenario.
 
El recorrido empieza viendo la existencia del `userFollow` (es la persona seguida), comparandolo con el usuario inicial; una vez encontrado, este es añadido en una primera posicion a una lista (Para la trazabilidad de pasos); el recorrido continua haciendo foco a los `follows` (aqui, registrando trazabilidad por vuelta) y esta comparandolos con el usuario final, una vez que encontro su objetivo, se apoya en los pasos trazados para obtener la distancia uno de otro.
 
 En todo flujo sea que se haga el recorrido o no, existe una obervacion de tiempo de procesamiento pudiendo saber cuanto demoro (Tener en cuenta que la primera vez, el tiempo es elevado, por el levantamiento del servicio como tal, luego de ello, los tiempo que den son los reales ).

La solucion se aterrizo con las siguientes caractaresticas:

Detalles:
    
* Visual Studio 2015 , Web Api Rest full C#
    
* Testeado por PostMan

## Get UsuarioMI

### Request

`GET api/UsuarioMI/`

    http://localhost:14983/api/UsuarioMI

| objDta (BODY)      |

| Nombre      | Tipo Dato     | Descripcion       | 
| ----------- | ------------- | --------- | 
| listFollow  | `List<Follow>` |  | 
| foUser      | FollowUser    |    |
    

| objDta (BODY) - RAW JSON      |
    
    {
      "listFollow": [
          {
              "userFollow": "userA",
              "follows": [
                  "userB",
                  "userD",
                  "userE",
                  "userG"
              ]
          },
          {
              "userFollow": "userB",
              "follows": [
                  "userC",
                  "userJ",
                  "userI",
                  "userE"
              ]
          },
          {
              "userFollow": "userC",
              "follows": [
                  "userM",
                  "userN",
                  "userJ",
                  "userI",
                  "userE"
              ]
          }
      ],
      "foUser": {
          "userInit": "userA",
          "userFin": "userM"
      }
    }



### Response

 | Nombre      | Tipo Dato     | Descripcion       | 
| ----------- | ------------- | --------- | 
| Distancia  | int  |  | 
| Pasos      | `List<string>`    |    |
| listaObservaciones      | `List<string>`    |    |
 
 
    {
     "$id": "1",
     "Distancia": 3,
     "Pasos": [
         "userA",
         "userB",
         "userC"
     ],
     "listObservaciones": [
         "Informativo: Peticion resuelta en 0 ms."
     ]
    }

Casos de Validacion Cubierto dentro de la solución:
* Error: No Existe informacion contra que realizar la busqueda.
* Error: La busqueda no cumple el parametro de usuarioInicial o usuarioFinal.
* Informativo: El usuarioInicial no se encuentra en los follows.
* Informativo: El usuarioFinal no se encuentra en los follows.
* Informativo: Busqueda no alcanzada, la distancia que desea calcular, no tiene follows.
* Error: Revise el Formato de la informacion.
* Informativo: Peticion resuelta en x ms.


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




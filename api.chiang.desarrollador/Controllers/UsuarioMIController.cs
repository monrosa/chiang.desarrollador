using MetaInformacion;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.chiang.desarrollador.Controllers
{
    public class UsuarioMIController : ApiController
    {
        [HttpGet]
        //public string Get([FromBody] List<Follow> listFollow, [FromBody]FollowUser foUser)
        public PasosDistancia Get([FromBody] JObject objDta)

        {
            DateTime? PeticionInicio = null;
            DateTime? PeticionFin = null;

            PeticionInicio = DateTime.Now;

            List<Follow> listFollow = new List<Follow>();
            PasosDistancia resp = new PasosDistancia();

            List<string> pasos = new List<string>();
            FollowUser foUser = new FollowUser();

            List<string> observaciones = new List<string>();

            bool aceptacionRecorrido = true;
            try
            {
                resp.Distancia = -1;
                resp.Pasos = null;

                dynamic jsonData = objDta;
                JObject JOfoUser = jsonData.foUser;
                JArray JAlistFollow = jsonData.listFollow;

                foUser = JOfoUser.ToObject<FollowUser>();

                JAlistFollow.ToList().ForEach(x => { listFollow.Add(x.ToObject<Follow>()); });

                //listFollow.ForEach( ox => { pasos.Add( (ox.userFollow.Equals(foUser.userInit) && pasos.Count == 0 ? ox.userFollow : (pasos.Count != 0) ? ox.userFollow : string.Empty ) ); });
                //resp.Distancia = -1;
                //resp.Pasos = null;

                if (listFollow.Count == 0)
                    observaciones.Add("Error: No Existe informacion contra que realizar la busqueda.");

                if(string.IsNullOrEmpty(foUser.userFin) || string.IsNullOrEmpty(foUser.userInit))
                    observaciones.Add("Error: La busqueda no cumple el parametro de usuarioInicial o usuarioFinal.");

                if (listFollow.Count != 0 && !string.IsNullOrEmpty(foUser.userFin) && !string.IsNullOrEmpty(foUser.userInit) )
                {
                    if(!listFollow.Exists(ex => ex.userFollow == foUser.userInit))
                    {
                        observaciones.Add("Informativo: El usuarioInicial no se encuentra en los follows.");
                        aceptacionRecorrido = false;
                    }

                    if (!listFollow.Exists(ex => string.Join(",",ex.follows).Contains(foUser.userFin) ))
                    {
                        observaciones.Add("Informativo: El usuarioFinal no se encuentra en los follows.");
                        aceptacionRecorrido = false;
                    }

                    if (aceptacionRecorrido)
                        foreach (var item in listFollow)
                        {
                            if (item.userFollow.Equals(foUser.userInit))
                                pasos.Add(item.userFollow);
                            else if (pasos.Count != 0)
                                pasos.Add(item.userFollow);

                            if (item.follows.Exists(x => x == foUser.userFin && pasos.Count != 0))
                                break;
                        }

                    if (aceptacionRecorrido)
                        if (pasos.Count != 0)
                        {
                            resp.Distancia = pasos.Count;
                            resp.Pasos = pasos;
                        }
                        else
                        {
                            //resp.Distancia = -1;
                            //resp.Pasos = null;
                            observaciones.Add("Informativo: Busqueda no alcanzada, la distancia que desea calcular, no tiene follows.");
                        }   
                }
                //resp.listObservaciones = observaciones;
            }
            catch (Exception e)
            {
                //resp.Distancia = -1;
                //resp.Pasos = null;
                observaciones.Add("Error: Revise el Formato de la informacion.");
                //resp.listObservaciones = new List<string> { "Error: Revise el Formato de la informacion." };
                //throw;
            }

            PeticionFin = DateTime.Now;

            TimeSpan? span = (PeticionFin - PeticionInicio);
            observaciones.Add($"Informativo: Peticion resuelta en {span.Value.Milliseconds} ms.");
            resp.listObservaciones = observaciones;
            return resp;
        }
    }
}

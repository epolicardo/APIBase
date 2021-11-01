
﻿using Serilog;

using System;
using System.Configuration;

namespace Configuration
{
    public class ConfigurationHelper : IConfigurationHelper
    {


        public string UrlServicio()
        {
            return ConfigurationManager.AppSettings.Get("API.Gastos");
        }

        public bool UseMockup(string Servicio)
        {
            try
            {

                if (ConfigurationManager.AppSettings.Get(Servicio) == ConfigurationManager.AppSettings.Get("AmbienteMockup")
                    && ConfigurationManager.AppSettings.Get("AmbienteMockup") == "true")
                    return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "No se encontró clave de Debug");
            }
            return false;
        }

        //public void mockupXML(string FileName)
        //{
        //    XmlSalida = new XmlDocument();
        //    XmlSalida.Load(Path.Combine(ObtenerPath() + "/DEBUG", FileName));
        //}
        //public void mockupJson(string FileName)
        //{
        //    XmlSalida = new XmlDocument();
        //    XmlSalida.Load(Path.Combine(ObtenerPath() + "/DEBUG", FileName));
        //}

    }
}

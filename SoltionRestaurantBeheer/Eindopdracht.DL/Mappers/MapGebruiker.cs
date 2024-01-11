using Eindopdracht.BL.Models;
using Eindopdracht.DL.Exceptions;
using Eindopdracht.DL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.DL.Mappers
{
    public class MapGebruiker
    {
        public static GebruikerEF MapToDB(Gebruiker gebruiker)
        {
            try
            {
                return new GebruikerEF(gebruiker.Klantnummer, gebruiker.Naam, gebruiker.Email, gebruiker.Telefoonnummer, gebruiker.Actief, gebruiker.Locatie.Postcode, gebruiker.Locatie.Gemeentenaam, gebruiker.Locatie.Straatnaam, gebruiker.Locatie.Huisnummer);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapGebruiker - MapToDB", ex);
            }
        }

        public static Gebruiker MapToDomain(GebruikerEF gebruikerEF)
        {
            try
            {
                Locatie locatie = new Locatie(gebruikerEF.Postcode, gebruikerEF.Gemeentenaam, gebruikerEF.Straatnaam, gebruikerEF.Huisnummer);

                return new Gebruiker(gebruikerEF.Klantnummer, gebruikerEF.Naam, gebruikerEF.Email, gebruikerEF.Telefoonnummer, gebruikerEF.Actief, locatie);
            }
            catch (Exception ex)
            {
                throw new MapperException("MapGebruiker - MapToDomain", ex);
            }
        }
    }
}
namespace PCentar
{
    internal class Domacin
    {
        // Clanovi porodice
        public int idDomacin { get; set; }

        public string imeDomacina { get; set; }
        public string prezimeDomacina { get; set; }
        public string deca { get; set; }

        // Adresa
        public string adresa { get; set; }

        public string mesto { get; set; }

        // Info
        public string nazivSlave { get; set; }

        public int ukupnoClanova { get; set; }
        public string bracnoStanje { get; set; }
        public string parohijal { get; set; }
        public string sumaParohijal { get; set; }
        public string primanjeZaVaskrs { get; set; }
        public string primanjeZaSlavu { get; set; }
        public string daLiSlavi { get; set; }
        public string zastoNeSlavi { get; set; }
        public string donosiZito { get; set; }
        public string zapazanja { get; set; }
        public string email { get; set; }
        public string telefon1 { get; set; }
        public string telefon2 { get; set; }
        public string poreklo { get; set; }
        public string datumUnosa { get; set; }

        public string PunoIme
        {
            get
            {
                return this.prezimeDomacina + " " + this.imeDomacina;
            }
        }
    }
}
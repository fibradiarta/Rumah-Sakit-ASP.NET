using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebRumahSakit.Models;

namespace WebRumahSakit.ViewModels
{
    public class RuanganViewModels
    {
        public RuanganViewModels() { }

        public RuanganViewModels(RUANGAN ruangan)
        {
            RUANGAN_ID = ruangan.RUANGAN_ID;
            NAMA = ruangan.NAMA;
            STATUS = ruangan.STATUS;
        }


        public int RUANGAN_ID { get; set; }
        public string NAMA { get; set; }
        public Nullable<bool> STATUS { get; set; }

        public string JENIS_RUANGAN_ID { get; set; }
    }
}
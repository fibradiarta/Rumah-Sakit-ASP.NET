using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebRumahSakit.Models;

namespace WebRumahSakit.ViewModels
{
    public class DokterViewModels
    {
        public DokterViewModels() { }

        public DokterViewModels(DOKTER dokter)
        {
            DOKTER_ID = dokter.DOKTER_ID;
            NAMA = dokter.NAMA;
            ALAMAT = dokter.ALAMAT;
            NO_TELP = dokter.NO_TELP;
            JENIS_KELAMIN = dokter.JENIS_KELAMIN;
            STATUS = dokter.STATUS;
        }

        public int DOKTER_ID { get; set; }
        public string NAMA { get; set; }
        public string ALAMAT { get; set; }
        public string NO_TELP { get; set; }
        public Nullable<bool> JENIS_KELAMIN { get; set; }
        public Nullable<bool> STATUS { get; set; }


        public Nullable<int> SPECIALIS_ID { get; set; }
        public Nullable<int> POLI_ID { get; set; }
    }
}
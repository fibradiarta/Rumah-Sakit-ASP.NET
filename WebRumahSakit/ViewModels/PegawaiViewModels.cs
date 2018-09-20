using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebRumahSakit.Models;

namespace WebRumahSakit.ViewModels
{
    public class PegawaiViewModels
    {
        public PegawaiViewModels() { }

        public PegawaiViewModels(PEGAWAI pegawai)
        {
            PEGAWAI_ID = pegawai.PEGAWAI_ID;
            NAMA = pegawai.NAMA;
            ALAMAT = pegawai.ALAMAT;
            NO_TELP = pegawai.NO_TELP;
            JENIS_KELAMIN = pegawai.JENIS_KELAMIN;
            STATUS = pegawai.STATUS;
            EMAIL = pegawai.EMAIL;
            PASSWORD = pegawai.PASSWORD;
        }

        public int PEGAWAI_ID { get; set; }
        public string NAMA { get; set; }
        public string ALAMAT { get; set; }
        public string NO_TELP { get; set; }
        public Nullable<bool> JENIS_KELAMIN { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }

        public Nullable<int> ROLE_ID { get; set; }
    }
}
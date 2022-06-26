using Ejercico1_4TomarFoto.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ejercico1_4TomarFoto.Controller
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbase;
        public DataBase(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);

            dbase.CreateTableAsync<Models.Foto>();
        }

        public Task<int> SavePhoto(Foto foto)
        {
            if (foto.id != 0)//update del registro
            {
                return dbase.UpdateAsync(foto);
            }
            else
            {
                return dbase.InsertAsync(foto);//inserter nuevo registro
            }

        } //tareas asincronas, no para la aplicacion se ejecuta la linea de codigo y se espera el resultado. Devuelve un valor de estado entero 0/1.

        public Task<List<Foto>> getListFoto()
        {
            return dbase.Table<Foto>().ToListAsync();//se convierte el resultado a una lista.
        }

    }
}

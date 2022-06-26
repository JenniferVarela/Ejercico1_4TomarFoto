using Ejercico1_4TomarFoto.Models;
using Ejercico1_4TomarFoto.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ejercico1_4TomarFoto
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        Plugin.Media.Abstractions.MediaFile Filefoto = null;
        private Byte[] ConvertImageToByteArray()
        {
            if (Filefoto != null)
            {
                using (MemoryStream memory = new MemoryStream()) //Declaramos que nuestro archivo estara en memoria ram 
                {
                    Stream stream = Filefoto.GetStream();//se convierte a string
                    stream.CopyTo(memory);//se copia en memoria
                    return memory.ToArray();//se convierte el string en array
                }

            }
            return null;

        }

        private async void tomarFoto_Clicked(object sender, EventArgs e)
        {

            //var
            Filefoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MisFotos",
                Name = "test.jpg",
                SaveToAlbum = true,
            });

            await DisplayAlert("path directorio", Filefoto.Path, "ok");

            if (Filefoto != null)
            {
                foto.Source = ImageSource.FromStream(() =>
                {
                    return Filefoto.GetStream();
                });
            }

        }
        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

            if (Filefoto == null)
            {
                await DisplayAlert("Error no se tomo la fotografia", "Aviso", "OK");
                return;
            }

            var foto = new Foto
            {
                id = 0,//porque si es diferente a 0 actualiza
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                foto = ConvertImageToByteArray(),
            };

            /* App.DBase.EmpleSave(emple) esto me retorna un resultado task y no puede ser validado con un entero*/
            var result = await App.DBase.SavePhoto(foto);//de esta manera convertimos el resultado de task a un entero

            if (result > 0)//se usa como una super clase
            {
                await DisplayAlert( "Aviso", "Registro exitoso", "OK");
            }
            else
            {
                await DisplayAlert("Aviso", "Error al Registrar",  "OK");
            }

        }

        private async void btnListar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListFoto());
        }
    }
}

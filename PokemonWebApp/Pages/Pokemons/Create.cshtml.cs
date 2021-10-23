using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokemonWebApp.Models; // adicionado referencia
using System.Net.Http; // adicionado dependencia
using System.Net.Http.Headers; // adicionado dependencia
using Newtonsoft.Json; // adicionado dependencia

namespace PokemonWebApp.Pages.Pokemons
{
    public class CreateModel : PageModel
    {
        [BindProperty]  // indica que quando for enviado (POST) compara o objeto Pokemon enviado do formulario com a classe modelo e insere esses dados no objeto abaixo
        public Pokemon Pokemon { get; set; }  // criado objeto para receber dados

        string baseUrl = "https://localhost:44391/"; // Url da API em formato string
        public async Task<IActionResult> OnPostAsync() // realiza a captura do Post 
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);  // cria uma variavel que converte a baseUrl em Uri
                client.DefaultRequestHeaders.Clear();  // limpa cabeçalho da requisição (só pra não consumir memória atoa)
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // comando que vai entender que o POST virá em formato json

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Pokemons", Pokemon); // cria um objeto que envia para um endereço (API) um outro objeto (Pokemon)
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return RedirectToPage("./Create");
                }
            }

        }
    }
}

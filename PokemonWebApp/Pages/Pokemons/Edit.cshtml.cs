using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokemonWebApp.Models;
using System.Net.Http; // adicionado dependencia
using System.Net.Http.Headers; // adicionado dependencia
using Newtonsoft.Json; // adicionado dependencia

namespace PokemonWebApp.Pages.Pokemons
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Pokemon Pokemon { get; set; }

        string baseUrl = "https://localhost:44391/"; // Url da API em formato string
        
        public async Task<IActionResult> OnGetAsync(int? id) // quando chegar um Get oriundo botão VER que está no pokemon, este recebe o valor determinado no botão
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id); // retorna a página com o resultado onde o Id do pokemon é igual a variável ID do botão editar na página de detalhes
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);  // converte a resposta da consulta em Json


                }

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() // funçao editar com o POST
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("api/Pokemons/" + Pokemon.ID, Pokemon); // executa o POST

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return Page();
                }


            }
        }

    }
}

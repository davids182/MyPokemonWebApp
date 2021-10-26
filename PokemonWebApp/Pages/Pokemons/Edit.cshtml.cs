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
        
        public async Task<IActionResult> OnGetAsync(int? id) // quando chegar um Get oriundo bot�o VER que est� no pokemon, este recebe o valor determinado no bot�o
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

                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id); // retorna a p�gina com o resultado onde o Id do pokemon � igual a vari�vel ID do bot�o editar na p�gina de detalhes
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);  // converte a resposta da consulta em Json


                }

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() // fun�ao editar com o POST
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

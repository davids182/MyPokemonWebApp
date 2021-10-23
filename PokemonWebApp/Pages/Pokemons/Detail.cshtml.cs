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
    public class DetailModel : PageModel
    {

        string baseUrl = "https://localhost:44391/"; // Url da API em formato string
        public Pokemon Pokemon { get; set; }
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

                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id); // retorna a página com o resultado onde o Id do pokemon é ugual a vaariável ID
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);  // converte a resposta da consulta em Json


                }

            }

                return Page();
        }
    }
}

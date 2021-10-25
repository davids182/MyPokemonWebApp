using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http; // inserido dependencia (Put/Post/Get/Delete)
using System.Net.Http.Headers; // inserido dependencia
using Newtonsoft.Json; // inserido dependencia
using PokemonWebApp.Models; // inserido dependencia

namespace PokemonWebApp.Pages.Pokemons
{
    public class DeleteModel : PageModel
    {
        [BindProperty] // compara os campos da página com o do banco de dados 
        public Pokemon Pokemon  { get; set; }
        string baseUrl = "https://localhost:44391/"; // Url da API em formato string
        public async Task<IActionResult> OnGetAsync(int? id) // quando usuario clicar no botão delete ele faz o submit e este recebe o POST e coloca no id
        {
            if(id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient()) //realiza a conexão com a Api 
            {

                client.BaseAddress = new Uri(baseUrl); // transforma a baseUrl de string em uma URI
                client.DefaultRequestHeaders.Clear(); // Limpa cabeçalho para não acumular dados desnecessários
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // define um header em formato Json 

                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id); // retorna a página com o resultado onde o Id do pokemon é igual a variável ID do botão da delete da página de detalhes
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result; // captura o conteúdo da resposta Json em formato string
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);  // converte a resposta da consulta em Json para um objeto do tipo pokemon

                }

            }

            return Page(); // retorna a página html
        }

        public async Task<IActionResult> OnPostAsync(int? id) // método que recebe o id do Post 
        {

            if(id == null) // se nulo 
            {

                return NotFound();

            }

            if(Pokemon.ID != id) // se pokemon id diferente do id enviado pelo POST
            {
                return BadRequest();
            }

            using (var client = new HttpClient()) // abre a conexão com a API
            {
                client.BaseAddress = new Uri(baseUrl); // transforma a baseUrl de string em uma URI
                client.DefaultRequestHeaders.Clear(); // Limpa cabeçalho para não acumular dados desnecessários
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // define um header em formato Json 

                HttpResponseMessage response = await client.DeleteAsync("api/Pokemons/" + Pokemon.ID); // envia o DELETE para remover o pokemon


                if (response.IsSuccessStatusCode) // se sucesso volta para index se não fica na mesma página
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

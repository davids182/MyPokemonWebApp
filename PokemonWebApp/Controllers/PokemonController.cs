﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonWebApp.Models; // adicionado dependencia
using System.Net.Http; // adicionado dependencia
using System.Net.Http.Headers; // adicionado dependencia
using Newtonsoft.Json; // adicionado dependencia

namespace PokemonWebApp.Controllers
{
    public class PokemonController : Controller
    {

        string baseUrl = "https://localhost:44391/"; // variável com o link do servidor base da API
        public async Task<ActionResult> Index()
        {
            List<Pokemon> pokemons = new List<Pokemon>();  // adiciona uma variável para receber os dados da lista de pokemons que será recebida

            using (var client = new HttpClient()) // variavel temporária (using) client que vai conectar na api
            {

                client.BaseAddress = new Uri(baseUrl); // cria uma variavel que converte a baseUrl em Uri
                client.DefaultRequestHeaders.Clear();  // limpa cabeçalho da requisição (só pra não consumir memória atoa)
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // comando que vai entender que a resposta virá em formato json

                HttpResponseMessage response = await client.GetAsync("api/Pokemons");   // este pega a resposta de forma get - "GetAsync" -  de um endereço que será adicionado ao baseUrl

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result; //recebe a string da api em json

                    pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(result); // efetua a quebra do Json em lista 

                }
            }


            return View(pokemons);
        }
    }
    
}

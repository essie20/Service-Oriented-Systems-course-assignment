using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private HttpClient mClient;
        public ShopController()
        {
            mClient = new HttpClient();
            mClient.BaseAddress = new Uri("https://localhost:44382/"); // Change to your API port
            mClient.DefaultRequestHeaders.Add("Accept", "application/json");
            mClient.DefaultRequestHeaders.Add("User-Agent", "ShopApp"); 
        }

        // GET: ShopController
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Shop>>> Index()
        {
            var response = await mClient.GetAsync("/api/Shops"); 

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                var content = await response.Content.ReadAsStringAsync();
                IEnumerable<Shop> shops = JsonConvert.DeserializeObject<IEnumerable<Shop>>(content); 
                return View(shops); 
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

        // GET: ShopController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind("Name,Owner,NumberOfWorkers,Director")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                var shopJson = JsonConvert.SerializeObject(shop);
                var stringContent = new StringContent(shopJson, Encoding.UTF8, "application/json");
                var response = await mClient.PostAsync("/api/Shops", stringContent);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
                }
            }

            return View(shop);
        }

        // GET: ShopController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await mClient.GetAsync("/api/Shops/" + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                Shop shop = JsonConvert.DeserializeObject<Shop>(content);
                return View(shop);
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Shop shop)
        {

            if (ModelState.IsValid)
            {
                var shopJson = JsonConvert.SerializeObject(shop);
                var stringContent = new StringContent(shopJson, Encoding.UTF8, "application/json");
                
                var response = await mClient.PutAsync("/api/Shops/" + id, stringContent);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
                }
            }

            return View(shop);

        }

        // GET: ShopController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await mClient.GetAsync("/api/Shops/" + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                Shop shop = JsonConvert.DeserializeObject<Shop>(content);
                return View(shop);
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

        // POST: ShopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Shop shop)
        {
            var response = await mClient.DeleteAsync("/api/Shops/" + id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            }

        }
    }
}

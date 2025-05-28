using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SchoolPortal.Common.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SchoolPortal.UI.Controllers
{
    public class StateController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StateController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("OcelotClient");
            _configuration = configuration;
        }

        // GET: State
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/v1/location/state");
                if (response.IsSuccessStatusCode)
                {
                    var states = await response.Content.ReadFromJsonAsync<List<ViewStateDto>>();
                    return View(states);
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to fetch state data.";
                    return View(new List<ViewStateDto>());
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unexpected error occurred while fetching data.";
                return View(new List<ViewStateDto>());
            }
        }

        // GET: State/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var stateResponse = await _httpClient.GetAsync($"/api/v1/location/state/{id}");
                var countriesResponse = await _httpClient.GetAsync("/api/v1/location/country");

                if (stateResponse.IsSuccessStatusCode && countriesResponse.IsSuccessStatusCode)
                {
                    var state = await stateResponse.Content.ReadFromJsonAsync<UpdateStateDto>();
                    var countries = await countriesResponse.Content.ReadFromJsonAsync<List<ViewCountryDto>>();
                    var country = countries?.FirstOrDefault(c => c.Id == state.CountryId);
                    ViewBag.CountryName = country?.CountryName ?? "N/A";
                    return View(state);
                }
                else
                {
                    TempData["ErrorMessage"] = "State not found.";
                    return NotFound();
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unexpected error occurred.";
                return NotFound();
            }
        }

        // GET: State/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/v1/location/country");
                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<List<ViewCountryDto>>();
                    ViewBag.Countries = countries;
                }
                return View();
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error loading countries.";
                return View();
            }
        }

        // POST: State/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStateDto state)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("/api/v1/location/state", state);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "State created successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error creating state. Please try again.";
                    }
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Unexpected error occurred while creating state.";
                }
            }

            // Reload countries for dropdown
            try
            {
                var response = await _httpClient.GetAsync("/api/v1/location/country");
                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<List<ViewCountryDto>>();
                    ViewBag.Countries = countries;
                }
            }
            catch { }

            return View(state);
        }

        // GET: State/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var stateResponse = await _httpClient.GetAsync($"/api/v1/location/state/{id}");
                var countriesResponse = await _httpClient.GetAsync("/api/v1/location/country");

                if (stateResponse.IsSuccessStatusCode && countriesResponse.IsSuccessStatusCode)
                {
                    var state = await stateResponse.Content.ReadFromJsonAsync<UpdateStateDto>();
                    var countries = await countriesResponse.Content.ReadFromJsonAsync<List<ViewCountryDto>>();
                    ViewBag.Countries = countries;
                    return View(state);
                }
                else
                {
                    TempData["ErrorMessage"] = "State not found.";
                    return NotFound();
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unexpected error occurred.";
                return NotFound();
            }
        }

        // POST: State/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UpdateStateDto state)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"/api/v1/location/state/{id}", state);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "State updated successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error updating state.";
                    }
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "Unexpected error occurred while updating state.";
                }
            }

            // Reload countries for dropdown
            try
            {
                var response = await _httpClient.GetAsync("/api/v1/location/country");
                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<List<ViewCountryDto>>();
                    ViewBag.Countries = countries;
                }
            }
            catch { }

            return View(state);
        }

        // GET: State/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var stateResponse = await _httpClient.GetAsync($"/api/v1/location/state/{id}");
                var countriesResponse = await _httpClient.GetAsync("/api/v1/location/country");

                if (stateResponse.IsSuccessStatusCode && countriesResponse.IsSuccessStatusCode)
                {
                    var state = await stateResponse.Content.ReadFromJsonAsync<UpdateStateDto>();
                    var countries = await countriesResponse.Content.ReadFromJsonAsync<List<ViewCountryDto>>();
                    var country = countries?.FirstOrDefault(c => c.Id == state.CountryId);
                    ViewBag.CountryName = country?.CountryName ?? "N/A";
                    return View(state);
                }
                else
                {
                    TempData["ErrorMessage"] = "State not found.";
                    return NotFound();
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unexpected error occurred.";
                return NotFound();
            }
        }

        // POST: State/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/v1/location/state/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "State deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Error deleting state.";
                    return NotFound();
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unexpected error occurred while deleting state.";
                return NotFound();
            }
        }
    }
} 
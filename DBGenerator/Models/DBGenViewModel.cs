using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DBGenerator.Models
{
    public class DBGenViewModel
    {
        [Required(ErrorMessage = "Wybierz silnik bazy danych.")]
        [Display(Name = "Wybierz silnik bazy danych:")]
        public EngineType? SelectedEngine {  get; set; }

        public IEnumerable<SelectListItem> EngineTypes { get; set; }

        [Required(ErrorMessage = "Wybierz bazę danych.")]
        [Display(Name = "Wybierz bazę danych:")]
        public int? SelectedDatabase { get; set; }

        public IEnumerable<SelectListItem> Databases { get; set; }

        public string Script { get; set; }
        public string YouTubeCode { get; set; }
        
    }
}

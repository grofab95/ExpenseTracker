using ExpenseTracker.Common;
using ExpenseTracker.Domain.Adapters;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ExpenseTracker.Web.Components
{
    public partial class AddCostComponent
    {
        [Inject] NotificationService notificationService { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        [Inject] ICategoryDao categoryDao { get; set; }
        [Inject] INameDao nameDao { get; set; }
        [Inject] ICostDao costDao { get; set; }

        private List<Category> _categories;
        private List<Name> _names;
        private List<Cost> _costs;
        private Cost _selectedCost;
        private Category _selectedCategory;

        private bool _useExistCost = true;
        private bool _isNewCategory = false;
        private bool _disabled = false;

        private string _description;
        private string _newName = null; 
        private string _newCategory = null; 
        private decimal _amount;

        protected override void OnInitialized()
        {
            _costs = GetExistedCosts();
            _categories = categoryDao.GetAll()
                .OrderByDescending(x => x.Costs.Count())
                .ToList();

            _names = nameDao.GetAll().ToList();
            _selectedCost = _costs.FirstOrDefault();

            if (_costs.Count > 0)
                _amount = _selectedCost.Amount;
        }

        private List<Cost> GetExistedCosts()
        {
            return costDao.GetAll()
                .OrderByDescending(q => q.CreatedAt).ToList()
                .GroupBy(x => x.Name.Id)
                .Select(y => new Cost
                {
                    Amount = y.Select(a => a.Amount).First(), 
                    Category = y.Select(c => c.Category).First(),
                    Name = y.Select(n => n.Name).First(),
                    Description = y.Select(d => d.Description).First(),
                    NumberOfUses = y.Count()
                })
                .OrderByDescending(z => z.NumberOfUses)
                .ToList();
        }

        private void UpdateDetails()
        {
            if (_costs.Count > 0)
            {
                _amount = _useExistCost ? _selectedCost.Amount : 0;
                _description = _useExistCost ? _selectedCost.Description?.Value : null;
            }
        }

        private void ChangeCostTypeHandler()
        {
            UpdateDetails();
        }

        private void PrepareFactors()
        {
            if (!string.IsNullOrEmpty(_newCategory))
            {
                _newCategory = new CultureInfo("en-US").TextInfo.ToTitleCase(_newCategory.Trim());
            }

            if (!string.IsNullOrEmpty(_newName))
            {
                _newName = _newName.Trim();
                _newName = char.ToUpper(_newName[0]) + _newName.Substring(1);
            }
        }

        private void PrepareForm()
        {
            _disabled = true;
            StateHasChanged();

            var task = Task.Run(async () => await AddCost());
        }

        private async Task AddCost()
        {
            try
            {
                PrepareFactors();
                var cost = new Cost();
                cost.Amount = _amount;

                if (_useExistCost)
                {
                    cost = _selectedCost;
                    cost.Amount = _amount;
                    cost.Description = _selectedCost.Description;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_newCategory) && _categories.Any(x => x.Name.ToLower() == _newCategory.ToLower()))
                    {
                        throw new Exception("Kategoria już istnieje");
                    }

                    cost.Category = _selectedCategory ?? new Category { Name = _newCategory };                                    
                    cost.Name = new Name { Value = _newName };

                    if (costDao.IsCostExit(cost))
                        throw new Exception("Już istnieje taki koszt.");

                    if (!string.IsNullOrEmpty(_description))
                    {
                        cost.Description = new Description { Value = _description };
                    }
                }

                costDao.AddCost(cost);

                Logger.Log<AddCostComponent>("Nowy wydatek został zapisany w bazie.", Common.LogLevel.INFO);
                await ShowNotification(new NotificationMessage
                {
                    Detail = "Wydatek został dodany.",
                    Duration = 3000,
                    Severity = NotificationSeverity.Success,
                    Summary = "Informacja"
                });

                await Task.Delay(2000);
                navigationManager.NavigateTo("/", true);
            }
            catch (Exception ex)
            {
                Logger.Log<AddCostComponent>(ex);
                _disabled = false;

                await ShowNotification(new NotificationMessage
                {
                    Detail = ex.Message,
                    Duration = 3000,
                    Severity = NotificationSeverity.Error,
                    Summary = "Informacja"
                });
            }
        }

        private async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);
            await InvokeAsync(() => { StateHasChanged(); });
        }
    }
}

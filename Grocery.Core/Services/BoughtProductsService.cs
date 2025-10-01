﻿
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            if (productId == null)
            {
                return null;
            }
            List<BoughtProducts> boughtProducts = new List<BoughtProducts>();
            foreach (var client in _clientRepository.GetAll())
            {
                Product product = _productRepository.Get((int)productId);
                GroceryList? groceryList = _groceryListRepository.GetAll().Where(g => g.ClientId == client.Id).FirstOrDefault();

                BoughtProducts bP = new BoughtProducts(client, groceryList, product);
                boughtProducts.Add(bP);
            }

            return boughtProducts;
        }
    }
}

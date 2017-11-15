using GroceryApp.Domain;
using GroceryApp.Request;
using GroceryApp.Response;
using GroceryApp.Services;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace GroceryApp.Controllers.API
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {

        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route(), HttpPost]
        public HttpResponseMessage ProductInsert(ProductAddRequest model)
        {
            try
            {
                Product response = new Product();
                response.Id = _productService.ProductInsert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage ProductUpdate(ProductUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _productService.ProductUpdate(model);
                return Request.CreateResponse(HttpStatusCode.OK, "Things were updated!");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage ProductDelete(int id)
        {
            try
            {
                _productService.ProductDelete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "it was deleted!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        [Route("{keyword}"), HttpGet]
        public HttpResponseMessage GetByProductKeyword(string keyword)
        {
            try
            {
                IEnumerable<Product> response = _productService.GetByProductKeyword(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("scrape"), HttpGet]
        public HttpResponseMessage ScrapePage()
        {
           
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString("https://techcrunch.com/"));
            var root = html.DocumentNode;

            IEnumerable<HtmlAgilityPack.HtmlNode> myList = new List<HtmlAgilityPack.HtmlNode>();
            myList = ((root.Descendants()
                .Where(n => n.GetAttributeValue("id", "").Equals("river1"))
                .Single()
                .Descendants("h2")));

            List<string> newList = new List<string>();

            foreach(HtmlNode itm in myList)
            {
                newList.Add(itm.InnerHtml);
            }
            

            return Request.CreateResponse(HttpStatusCode.OK, newList);

        }
    }
}

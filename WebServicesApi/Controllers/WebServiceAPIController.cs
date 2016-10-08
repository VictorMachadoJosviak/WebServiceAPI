using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebServicesApi.Models;

namespace WebServicesApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/service")]
    public class WebServiceAPIController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        [Route("products")]
        public HttpResponseMessage GetProducts()
        {
            try
            {
                //var list = db.Products.Include("Category").ToList(); opitional

                var list = db.Products.ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, list);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            try
            {
                var list = db.Categories.ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, list);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("categories/{CategoryId}/products")]
        public HttpResponseMessage GetProductsByCategory(int CategoryId)
        {
            try
            {
                var list = db.Products.Include("Category").Where(x => x.CategoryId == CategoryId).ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, list);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nada encontrado");
            }
        }

        [HttpGet]
        [Route("products/{Title}/products")]
        public HttpResponseMessage GetProductsByName(string Title)
        {
            try
            {
                var list = db.Products.Include("Category").Where(x => x.Title == Title).ToList();

                var response = Request.CreateResponse(HttpStatusCode.OK, list);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Nada encontrado");
            }
        }

        [HttpPost]
        [Route("postProduct")]
        public HttpResponseMessage PostProduct(Product prod)
        {
            try
            {
                var error = Request.CreateResponse(HttpStatusCode.BadRequest, "erro ao salvar");

                var insert = db.Products.Add(prod);
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, insert);

                var result = prod == null ? error : response;

                return result;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "erro ao salvar");
            }
        }

        [HttpPost]
        [Route("postcategory")]
        public HttpResponseMessage PostCategory(Category category)
        {
            try
            {
                if (category == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "objeto não pode ser nulo");

                var insert = db.Categories.Add(category);
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, insert);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "erro ao salvar");
            }
        }

        [HttpPut]
        [Route("putProduct")]
        public HttpResponseMessage PutProduct(Product product)
        {
            try
            {
                if (product == null) return Request.CreateResponse(HttpStatusCode.BadRequest, "erro ao salvar");

                var edit = db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, edit);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "erro ao salvar");
            }
        }

        [HttpPut]
        [Route("putcategory")]
        public HttpResponseMessage PutCategory(Category category)
        {
            try
            {
                if (category == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "erro ao salvar");

                var result = db.Entry<Category>(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo de errado nao esta certo");
            }
        }

        [HttpDelete]
        [Route("deleteproduct/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "erro ao excluir");

                var remove = db.Products.Remove(db.Products.Find(id));
                db.SaveChanges();
                var response = Request.CreateResponse(HttpStatusCode.OK, "Produto removido");

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo de errado nao esta certo");
            }
        }

        [HttpDelete]
        [Route("deletecategory/{id}")]
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "erro ao excluir");

                var remove = db.Categories.Remove(db.Categories.Find(id));
                db.SaveChanges();
                var response = Request.CreateResponse(HttpStatusCode.OK, "categoria removida");

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Algo de errado nao esta certo");
            }
        }
    }
}
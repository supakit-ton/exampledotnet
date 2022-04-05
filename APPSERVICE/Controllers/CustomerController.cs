using APPSERVICE.Models;
using DAL.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace APPSERVICE.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;


        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        [Route("getcustomer/{lang}")]
        public async Task<IActionResult> GetCustomer(string lang)
        {
            ResponseWithModel<List<CustomerModel>> response;
            try
            {
                lang = !string.IsNullOrEmpty(lang) ? lang.ToLower() : "th";

                var dbo_customer = await _customerRepo.GetCustomer();
                List<CustomerModel> customers = (from dbo in dbo_customer
                                                 select new CustomerModel
                                                 {
                                                     CustomerID = dbo.CustomerID,
                                                     FirstName = lang == "th" ? dbo.FirstName_TH : dbo.FirstName_EN,
                                                     LastName = lang == "th" ? dbo.LastName_TH : dbo.LastName_EN,
                                                     Age = dbo.Age,
                                                     Email = dbo.Email,
                                                     MobileNo = dbo.MobileNo
                                                 }).ToList();

                response = new ResponseWithModel<List<CustomerModel>>
                {
                    result = "01",
                    message = "success.",
                    data = customers
                };

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response = new ResponseWithModel<List<CustomerModel>>
                {
                    result = "99",
                    message = ex.Message,
                    data = null
                };

                return StatusCode(500, response);
            }
        }

        [HttpGet]
        [Route("getcustomerdetail/{id}/{lang}")]
        public async Task<IActionResult> GetCustomerDetail(int id, string lang)
        {
            ResponseWithModel<CustomerModel> response;
            try
            {
                lang = !string.IsNullOrEmpty(lang) ? lang.ToLower() : "th";

                var dbo_customer = await _customerRepo.GetCustomerByID(id);
                if (dbo_customer is null || dbo_customer.CustomerID == 0)
                {
                    response = new ResponseWithModel<CustomerModel>
                    {
                        result = "02",
                        message = "customer not found.",
                        data = null
                    };
                    return StatusCode(404, response);
                }

                CustomerModel customer = new CustomerModel
                {
                    CustomerID = dbo_customer.CustomerID,
                    FirstName = lang == "th" ? dbo_customer.FirstName_TH : dbo_customer.FirstName_EN,
                    LastName = lang == "th" ? dbo_customer.LastName_TH : dbo_customer.LastName_EN,
                    Age = dbo_customer.Age,
                    Email = dbo_customer.Email,
                    MobileNo = dbo_customer.MobileNo
                };

                response = new ResponseWithModel<CustomerModel>
                {
                    result = "01",
                    message = "success.",
                    data = customer
                };

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response = new ResponseWithModel<CustomerModel>
                {
                    result = "99",
                    message = ex.Message,
                    data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Route("addcustomer")]
        public async Task<IActionResult> AddCustomer(tb_customer _customer)
        {
            ResponseWithModel<CustomerResponse> response;
            try
            {
                DateTime datetimenow = DateTime.Now;

                _customer.CreateDate = datetimenow;
                _customer.ModifyDate = datetimenow;

                await _customerRepo.Add(_customer);
                await _customerRepo.SaveAsync();

                response = new ResponseWithModel<CustomerResponse>
                {
                    result = "01",
                    message = "success.",
                    data = new CustomerResponse
                    {
                        UpdateStatus = true,
                        Messagelabel = "บันทึกข้อมูลสำเร็จ"
                    }
                };

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response = new ResponseWithModel<CustomerResponse>
                {
                    result = "99",
                    message = ex.Message,
                    data = null
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Route("removecustomer")]
        public async Task<IActionResult> RemoveCustomer([FromBody] int _customerID)
        {
            ResponseWithModel<CustomerResponse> response;
            try
            {
                var dbo_customer = await _customerRepo.GetCustomerByID(_customerID);
                if (dbo_customer is null || dbo_customer.CustomerID == 0)
                {
                    response = new ResponseWithModel<CustomerResponse>
                    {
                        result = "01",
                        message = "success",
                        data = new CustomerResponse
                        {
                            UpdateStatus = false,
                            Messagelabel = "ไม่พบข้อมูล customer"
                        }
                    };
                    return StatusCode(404, response);
                }

                _customerRepo.Remove(dbo_customer);
                await _customerRepo.SaveAsync();

                response = new ResponseWithModel<CustomerResponse>
                {
                    result = "01",
                    message = "success.",
                    data = new CustomerResponse
                    {
                        UpdateStatus = true,
                        Messagelabel = "บันทึกข้อมูลสำเร็จ"
                    }
                };

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response = new ResponseWithModel<CustomerResponse>
                {
                    result = "99",
                    message = ex.Message,
                    data = null
                };
                return StatusCode(500, response);
            }
        }
    }
}

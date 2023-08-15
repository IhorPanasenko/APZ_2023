package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.Company
import retrofit2.http.GET

interface CompaniesService {
    @GET("api/Company/GetAll")
    suspend fun getCompanies(): List<Company>
}
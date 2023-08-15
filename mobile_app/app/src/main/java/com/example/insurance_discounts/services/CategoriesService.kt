package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.Category
import retrofit2.Call
import retrofit2.http.GET

interface CategoriesService {
    @GET("api/Category/GetAll")
    suspend fun getAllCategories(): List<Category>
}
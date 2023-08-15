package com.example.insurance_discounts.clients

import com.example.insurance_discounts.data.Category
import com.example.insurance_discounts.data.Insurance
import com.example.insurance_discounts.services.CategoriesService
import okhttp3.OkHttpClient
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object CategoriesClient {
    private val BASE_URL = "https://10.0.2.2:7082/"

    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .client(okHttpClient.createOkHttpClient())
            .build()
    }

    val service: CategoriesService by lazy {
        retrofit.create(CategoriesService::class.java)
    }
}
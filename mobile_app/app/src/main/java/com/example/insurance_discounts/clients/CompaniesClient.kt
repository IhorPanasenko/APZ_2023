package com.example.insurance_discounts.clients

import com.example.insurance_discounts.services.CompaniesService
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object CompaniesClient {
    private const val BASE_URL = "https://10.0.2.2:7082/"

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl(BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .client(okHttpClient.createOkHttpClient())
        .build()

    val companiesService: CompaniesService = retrofit.create(CompaniesService::class.java)
}

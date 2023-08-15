package com.example.insurance_discounts.clients

import com.example.insurance_discounts.services.AgentsService
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object AgentsClient {
    private const val BASE_URL = "https://10.0.2.2:7082/"

    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .client(okHttpClient.createOkHttpClient())
            .build()
    }

    val service: AgentsService by lazy {
        retrofit.create(AgentsService::class.java)
    }
}
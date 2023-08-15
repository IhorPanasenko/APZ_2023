package com.example.insurance_discounts.clients

import com.example.insurance_discounts.services.UpdateUserService
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object UpdateUserClient {
    private const val BASE_URL = "https://10.0.2.2:7082/"

    val service: UpdateUserService by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .client(okHttpClient.createOkHttpClient())
            .build()
            .create(UpdateUserService::class.java)
    }
}

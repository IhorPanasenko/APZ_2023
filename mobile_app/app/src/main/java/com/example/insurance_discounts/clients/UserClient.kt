package com.example.insurance_discounts.clients

import com.example.insurance_discounts.services.UserService
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object UserClient {
    private const val BASE_URL = "https://10.0.2.2:7082/"

    fun create(): UserService {
        val retrofit = Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .client(okHttpClient.createOkHttpClient())
            .build()

        return retrofit.create(UserService::class.java)
    }
}

package com.example.insurance_discounts.clients

import com.example.insurance_discounts.data.RegisterRequest
import com.example.insurance_discounts.services.RegisterService
import com.google.gson.GsonBuilder
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class RegisterClient {
    private val retrofit: Retrofit by lazy {
        val gson = GsonBuilder().setLenient().create()
        Retrofit.Builder()
            .baseUrl("https://10.0.2.2:7082/")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(okHttpClient.createOkHttpClient())
            .build()
    }

    private val registerService: RegisterService  by lazy {
        retrofit.create(RegisterService::class.java)
    }

    suspend fun registerUser(request: RegisterRequest): Response<String> {
        return registerService.registerUser(request)
    }

}
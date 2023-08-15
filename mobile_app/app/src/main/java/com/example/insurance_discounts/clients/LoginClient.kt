package com.example.insurance_discounts.clients

import com.example.insurance_discounts.data.LoginRequest
import com.example.insurance_discounts.data.LoginResponse
import com.example.insurance_discounts.services.LoginService
import com.google.gson.GsonBuilder
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class LoginClient {
    private val retrofit: Retrofit by lazy {
        val gson = GsonBuilder().setLenient().create()
        Retrofit.Builder()
            .baseUrl("https://10.0.2.2:7082/")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(okHttpClient.createOkHttpClient())
            .build()
    }

    private val loginService: LoginService by lazy {
        retrofit.create(LoginService::class.java)
    }

    suspend fun login(email: String, password: String): Response<String> {
        val loginRequest = LoginRequest(email, password)
        return loginService.login(loginRequest)
    }
}
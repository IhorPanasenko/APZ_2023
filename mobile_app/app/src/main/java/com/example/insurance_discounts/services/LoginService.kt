package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.LoginRequest
import com.example.insurance_discounts.data.LoginResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST

interface LoginService {
    @POST("api/Account/Login")
    suspend fun login(@Body loginRequest: LoginRequest): Response<String>
}
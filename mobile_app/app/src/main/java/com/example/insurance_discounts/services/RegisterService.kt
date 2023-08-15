package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.RegisterRequest
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST

interface RegisterService {
    @POST("api/Account/Register")
    suspend fun registerUser(@Body request: RegisterRequest): Response<String>
}
package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.UpdateUserRequest
import retrofit2.http.Body
import retrofit2.http.POST

interface UpdateUserService {
    @POST("api/User/Update")
    suspend fun updateUser(@Body request: UpdateUserRequest)
}
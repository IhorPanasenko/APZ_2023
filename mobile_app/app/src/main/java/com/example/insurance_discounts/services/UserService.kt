package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.UserInfo
import com.example.insurance_discounts.data.UserInsurance
import com.example.insurance_discounts.data.UserPolicyRequest
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface UserService {
    @GET("api/User/GetById")
    fun getUserInfo(@Query("userId") userId: String): Call<UserInfo>

    @GET("api/UserPolicy/GetByUserId")
    fun getUserInsurances(@Query("userId") userId: String): Call<List<UserInsurance>>

    @POST("api/UserPolicy/Create")
    suspend fun createUserPolicy(@Body requestBody: UserPolicyRequest)
}
package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.Insurance
import com.example.insurance_discounts.data.InsurancesResponse
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.GET

interface InsurancesService {
        @GET("/api/Policy/GetAll")
        fun getInsurances(): Call<List<Insurance>>
}
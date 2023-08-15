package com.example.insurance_discounts.clients

import com.example.insurance_discounts.data.Insurance
import com.example.insurance_discounts.data.InsurancesResponse
import com.example.insurance_discounts.services.InsurancesService
import com.google.gson.GsonBuilder
import retrofit2.Call
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class InsurancesClient {
    private val insurancesService: InsurancesService

    init {
        val retrofit = Retrofit.Builder()
            .baseUrl("https://10.0.2.2:7082/")
            .addConverterFactory(GsonConverterFactory.create())
            .client(okHttpClient.createOkHttpClient())
            .build()

        insurancesService = retrofit.create(InsurancesService::class.java)
    }

    fun getAllInsurances(): Call<List<Insurance>>{
        return insurancesService.getInsurances()
    }

}
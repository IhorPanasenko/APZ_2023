package com.example.insurance_discounts.services

import com.example.insurance_discounts.data.Agent
import retrofit2.http.GET

interface AgentsService {
    @GET("api/Agent/GetAll")
    suspend fun getAgents(): List<Agent>
}
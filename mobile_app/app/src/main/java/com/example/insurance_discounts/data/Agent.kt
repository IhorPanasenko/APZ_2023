package com.example.insurance_discounts.data

data class Agent(
    val id: String,
    val firstName: String,
    val secondName: String,
    val phoneNumber: String,
    val emailAddress: String,
    val raiting: Float,
    val companyId: String,
    val company: Company
)

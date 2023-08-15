package com.example.insurance_discounts.data

data class Insurance(
    val id: String,
    val name: String,
    val description: String,
    val coverageAmount: Double,
    val price: Double,
    val timePeriod: Int,
    val companyId: String,
    val company: Company
)

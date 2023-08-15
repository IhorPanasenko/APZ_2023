package com.example.insurance_discounts.data

data class Company(
    val id: String,
    val companyName: String,
    val description: String,
    val address: String,
    val phoneNumber: String,
    val emailAddress: String,
    val websiteAddress: String,
    val maxDiscountPercentage: Int
)
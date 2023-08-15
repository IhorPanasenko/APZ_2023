package com.example.insurance_discounts.data

data class UpdateUserRequest(
    val id: String,
    val firstName: String,
    val lastName: String,
    val userName: String,
    val phoneNumber: String,
    val address: String
)

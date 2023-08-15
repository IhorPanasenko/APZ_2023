package com.example.insurance_discounts.data

data class UserPolicyRequest(
    val startDate: String,
    val endDate: String,
    val userId: String,
    val policyId: String
)

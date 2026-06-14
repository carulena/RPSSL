using RpsslGameApi.Contracts;

namespace RpsslGameApi.Infrastructure.Mappers.Interfaces;

public interface IPlayMapper
{
    public PlayResponse Play(int randomNumber, int player);
}
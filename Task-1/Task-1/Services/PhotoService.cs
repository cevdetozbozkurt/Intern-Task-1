﻿using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using Task_1.Helpers;
using Task_1.Interfaces;

namespace Task_1.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly Cloudinary _cloudinary;

		public PhotoService(IOptions<CloudinarySettings> config)
		{
			var acc = new Account(
				config.Value.CloudName,
				config.Value.ApiKey,
				config.Value.ApiSecret
				);
			_cloudinary = new Cloudinary(acc);
		}
		public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
		{
			var uploadResult = new ImageUploadResult();
			if (file.Length > 0)
			{
				using var stream = file.OpenReadStream();
				var uploadParams = new ImageUploadParams
				{
					File = new FileDescription(file.Name, stream),
					Transformation = new Transformation().Height(231).Width(411).Crop("fill").Gravity("face")
				};
				uploadResult = await _cloudinary.UploadAsync(uploadParams);
			}
			return uploadResult;
		}

		public async Task<DeletionResult> DeletePhotoAsync(string publicId)
		{
			var deleteParams = new DeletionParams(publicId);
			var result = await _cloudinary.DestroyAsync(deleteParams);
			return result;
		}
	}
}
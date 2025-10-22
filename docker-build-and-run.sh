#!/bin/bash

# Docker build and run script for Pump Test Factory

echo "🐳 Building Docker image for Pump Test Factory..."
docker build -t pump-test-factory .

if [ $? -eq 0 ]; then
    echo "✅ Docker image built successfully!"
    echo ""
    echo "🚀 Usage examples:"
    echo "  docker run --rm pump-test-factory pump opened"
    echo "  docker run --rm pump-test-factory compressor start"
    echo "  docker run --rm pump-test-factory valve test"
    echo ""
    echo "📋 Running example test (pump opened)..."
    docker run --rm pump-test-factory pump opened
else
    echo "❌ Docker build failed!"
    exit 1
fi